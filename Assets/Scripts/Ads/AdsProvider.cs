using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

namespace Game
{
    public class AdsProvider : MonoBehaviour
    {
        [SerializeField] private int _delayIntro;

        public int _rewardIndex = -1; // 0 - Всплывающее окно с рекламмой. 1 - Случайное количество ресурсов. 2 - Рекламма для спавна + 2 героя

        private float _timer;
        private float _timerPause = 2f;

        public static AdsProvider Instance { get; private set; }
        private void Awake()
        {
            if (!Instance)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        private void Update()
        {
            if (!InApp.Instance.ShowAds) return;
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Intro();
            }else
            {
                _timer = 0;
            }
        }

        public void RewardAds(int index) // Логика выбора рекламмы
        {
            if (_rewardIndex == -1)
            {
                _rewardIndex = index;
                YandexGame.RewVideoShow(0);
            }
        }
        public void ShowAds(int index) // Устанавливаем на кнопки с рекламой и указываем индекс
        {
            if (_rewardIndex != -1)
            {
                _rewardIndex = -1;
            }
            RewardAds(index);
        }
        public void AddMoney()
        {
            if (_rewardIndex == 0)
            {
                SetupAds.Instance.WindowAds.AddMoneyAds();
                _rewardIndex = -1;
            }
        }

        public void AddRandomRes()
        {
            if (_rewardIndex == 1)
            {
                SetupAds.Instance.UIRecources.AddRandomRes();
                _rewardIndex = -1;
            }
        }

        public void SpawnNextHero()
        {
            if (_rewardIndex == 2)
            {
                SetupAds.Instance.SpawnHero.AdsSpawn();
                _rewardIndex = -1;
            }
        }

        private void Intro()
        {
            _timer += Time.deltaTime;

            if (_timer >= _delayIntro)
            {
                Time.timeScale = 0f;
                SetupAds.Instance.WindowIntro.Show();
                SetupAds.Instance.WindowIntro.SetTimer(_timerPause);
                _timerPause -= Time.unscaledDeltaTime;
                if (_timerPause <= 0)
                {
                    ShowIntroAds();
                    _timerPause = 2f;
                    _timer = 0f;
                    SetupAds.Instance.WindowIntro.Hide();
                }
            }
        }

        private void ShowIntroAds()
        {
            YandexGame.FullscreenShow();
        }
    }
}
