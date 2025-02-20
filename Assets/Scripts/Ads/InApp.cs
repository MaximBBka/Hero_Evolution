using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject.Asteroids;

namespace Game
{
    public class InApp : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void BayNoAds();
        [DllImport("__Internal")]
        private static extern void HasBayNoAds();
        [DllImport("__Internal")]
        private static extern void DeleteNoAds();

        [SerializeField] private Button _buttonBay;
        [SerializeField] private TextMeshProUGUI _log;

        private WaitForSeconds _wait;

        public bool ShowAds { get; private set; } = true;
        public static InApp Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else
            {
                Instance = this;
                if (YandexGame.SDKEnabled)
                {
                    HasBay();
                    return;
                }
                StartCoroutine(WaiteSdkInit());
                return;
            }
        }

        public void HasBay()
        {
            try
            {
                HasBayNoAds();
                _log?.SetText($"{_log.text}\n No Ads {(ShowAds ? "Ещё не куплена." : " уже куплена.")}");
            }
            catch
            {
                _log?.SetText($"{_log.text}\n если видно это сообщение, то произошла ошибка соединения с магазином.");
            }
        }

        private IEnumerator WaiteSdkInit()
        {
            _log?.SetText($"{_log.text}\n Ожидание инициализации SKD Yandex");

            while (!YandexGame.SDKEnabled)
            {
                yield return new WaitForSeconds(1f);
            }

            _log?.SetText($"{_log.text}\n SKD Yandex инициализирован.");
            HasBay();
        }

        private IEnumerator WaitePlayerData()
        {
            _wait ??= new(1f);

            while (true)
            {
                IsActive();
                yield return _wait;
            }
        }

        public void Bay()
        {
            _log?.SetText($"{_log.text}\n Попытка покупки.");

            if (ShowAds)
            {
                try
                {
                    BayNoAds();
                    _log?.SetText($"{_log.text}\n {(ShowAds ? "Покупка удалась!" : " покупка не удалась!")}");
                }
                catch
                {
                    _log?.SetText($"{_log.text}\n если видно это сообщение, то произошла ошибка соединения с магазином при покупке.");
                }
            }

            _buttonBay.gameObject.SetActive(false);
            StartCoroutine(WaitePlayerData());
        }

        public void BayTrue()
        {
            _log?.SetText($"{_log.text}\n Очень важно!Ответ из магазина в клиент прибыл! Покупка успешно завершена!");
            ShowAds = false;
            IsActive();
        }

        public void DeleteBay()
        {
            _log?.SetText($"{_log.text}\n Попытка удаления покупки.");
            DeleteNoAds();
        }

        public void DeleteBayTrue()
        {
            _log?.SetText($"{_log.text}\n Ответ от магазина,покупка удалена!");
            ShowAds = true;
        }

        private void IsActive()
        {
            _buttonBay.gameObject.SetActive(ShowAds);
        }
    }
}
