using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class WindowAncoument : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textTitle;               
        [SerializeField] private TextMeshProUGUI _textReward1;                              
        [SerializeField] private TextMeshProUGUI _textReward2;                              
        [SerializeField] private Transform _panelAncoument;
        [SerializeField] private Transform _panelStars;
        [SerializeField] private Transform _particle1;
        [SerializeField] private Transform _particle2;
        [SerializeField] private SOReward _reward;
        
        private int _strongPlayer;
        private int _moneyPlayer;
        private int _rewardMoney1;
        private int _rewardMoney2;
        private int _rewardStrong1;
        private int _rewardStrong2;
        
        private void Start()
        {
            Load();
        }
        private void Load()
        {
            YandexGame.LoadProgress();
            _strongPlayer = YandexGame.savesData.Strong;
            _moneyPlayer = YandexGame.savesData.Money;
        }
        private void Save(int strong, int money)
        {
            YandexGame.savesData.Strong += strong;
            YandexGame.savesData.Money += money;
            YandexGame.SaveProgress();
        }
        public void ShowWin()
        {
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Win);
            _panelAncoument.gameObject.SetActive(true);
            _panelStars.gameObject.SetActive(true);
            _particle1.gameObject.SetActive(true);
            _particle2.gameObject.SetActive(true);
            if (YandexGame.EnvironmentData.language == "ru")
            {
                _textTitle.SetText($"Победа!");               
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                _textTitle.SetText($"Victory!");
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                _textTitle.SetText($"Zafer!!");
            }
            
            SetReward();
            if (_rewardMoney1 > 1000)
            {
                int thousands = _rewardMoney1 / 1000;
                int hundreds = (_rewardMoney1 % 1000) / 100;
                _textReward1.text = $"{thousands}.{hundreds}K";
            }
            else
            {
                _textReward1.text = $"{_rewardMoney1}";
            }
            _textReward2.SetText($"{_rewardStrong1}");
            Save(_rewardStrong1 , _rewardMoney1);
        }

        public void ShowLose()
        {
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Lose);
            _panelAncoument.gameObject.SetActive(true);
            _panelStars.gameObject.SetActive(false);
            _particle1.gameObject.SetActive(false);
            _particle2.gameObject.SetActive(false);
            if (YandexGame.EnvironmentData.language == "ru")
            {
                _textTitle.SetText($"Поражение!");
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                _textTitle.SetText($"Defeat!");
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                _textTitle.SetText($"Yenilgi!");
            }         
            SetReward();
            _textReward1.SetText($"-{_rewardMoney2}");
            _textReward2.SetText($"-{_rewardStrong2}");
            Save(-_rewardStrong2, -_rewardMoney2);
        }

        private void SetReward()
        {
            for(int i = _reward.Rewards.Length - 1; i >= 0; i--)
            {
                if (_strongPlayer >= _reward.Rewards[i].Strong)
                {
                    _rewardMoney1 = _reward.Rewards[i].AddMoney;
                    _rewardMoney2 = (_moneyPlayer * 40) / 100;
                    _rewardStrong1 = _reward.Rewards[i].AddStrong;
                    _rewardStrong2 = _reward.Rewards[i].RemoveStrong;
                }
            }
        }
        public void GoHome()
        {
            SceneManager.LoadScene(0);
        }
    }
}
