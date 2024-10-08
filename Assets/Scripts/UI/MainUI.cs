using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class MainUI : MonoBehaviour
    {
        [field: SerializeField] public int _money { get; private set; } = 40;
        [field: SerializeField] public int _totalStrong { get; private set; }
        [field: SerializeField] public Slider _upMoney { get; private set; }



        [SerializeField] private TextMeshProUGUI _textMoney;
        [SerializeField] private TextMeshProUGUI _textStrong;
        [SerializeField] private Transform _windowTutorial;


        private int _totalUpMoney = 4;
        private float _speedSliderDown = 0.1f;
        private int _maxStrong;
        private bool _isFirstSession;

        public int MultiplyMoney = 1;
        public Image ImagePrice;
        public Image ImageAds;
        public TextMeshProUGUI TextPrice;
        private void Awake()
        {
            Load();
        }
        public void ShowAds(int index) // Устанавливаем на кнопки с рекламой и указываем индекс
        {
            if (AdsProvider.Instance._rewardIndex != -1)
            {
                AdsProvider.Instance._rewardIndex = -1;
            }
            AdsProvider.Instance.RewardAds(index);
        }
        private void Load()
        {
            YandexGame.LoadProgress();
            _totalStrong = YandexGame.savesData.Strong;
            _maxStrong = YandexGame.savesData.MaxStrong;
            _money = YandexGame.savesData.Money;
            _isFirstSession = YandexGame.savesData.isFirstOpen;
            UpdateInfo();
        }
        private void Save()
        {
            YandexGame.savesData.Strong = _totalStrong;            
            YandexGame.savesData.Money = _money;                      
            YandexGame.savesData.isFirstOpen = _isFirstSession;                      
            if (_maxStrong < _totalStrong)
            {
                _maxStrong = _totalStrong;
                YandexGame.savesData.MaxStrong = _maxStrong;
                AddLeaderBoard(_maxStrong);
            }
            YandexGame.SaveProgress();
        }

        private void Start()
        {
            if(_isFirstSession)
            {                
                _windowTutorial.gameObject.SetActive(true);
            }
            StartCoroutine(SliderDown());
        }
        public void CloseTutorial()
        {
            _isFirstSession = false;
            _windowTutorial.gameObject.SetActive(false);
            Save();
        }
        public void AddMoneyMultiply(int money)
        {
            _money += money * MultiplyMoney;
            if (_money <= 0)
            {
                _money = 0;
            }
            UpdateInfo();
        }
        public void AddMoney(int money)
        {
            _money += money;
            if(_money <= 0)
            {
                _money = 0;
            }
            UpdateInfo();
        }
        public void AddStrong(int strong)
        {
            _totalStrong += strong;
            UpdateInfo();
            Save();
        }
        public void UpdateInfo()
        {
            if (_totalStrong > 1000)
            {
                int thousands = _totalStrong / 1000;
                int hundreds = (_totalStrong % 1000) / 100;
                _textStrong.text = $"{thousands}.{hundreds}K";
            }
            else
            {
                _textStrong.text = $"{_totalStrong}";
            }
            if (_money > 1000)
            {
                int thousands = _money / 1000;
                int hundreds = (_money % 1000) / 100;
                _textMoney.text = $"{thousands}.{hundreds}K";
            }
            else
            {
                _textMoney.text = $"{_money}";
            }
        }
        public void UpMoney()
        {
            _upMoney.value += _totalUpMoney;
        }

        public IEnumerator SliderDown()
        {
            while (true)
            {
                _upMoney.value -= _speedSliderDown;
                yield return new WaitForSeconds(0.01f);
                switch ((int)_upMoney.value)
                {
                    case 0:
                        MultiplyMoney = 1;
                        break;
                    case 25:
                        MultiplyMoney = 2;
                        break;
                    case 55:
                        MultiplyMoney = 3;
                        break;
                    case 85:
                        MultiplyMoney = 4;
                        break;

                }
            }
        }

        public void AddLeaderBoard(int total)
        {
            YandexGame.NewLeaderboardScores("LiderBoardStrong", total);
        }
        private void OnDestroy()
        {
            Save();
        }
    }
}
