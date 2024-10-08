using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class WindowBattle : MonoBehaviour
    {
        [SerializeField] private Transform _panel;
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textTimer;
        [SerializeField] private TextMeshProUGUI _textNickName;
        [SerializeField] private string[] _nickname;

        private MainUI _mainUI;
        private SpawnHero _spawnHero;


        [Inject]
        public void Construct(MainUI ui, SpawnHero spawnHero)
        {
            _mainUI = ui;
            _spawnHero = spawnHero;
        }
        private float _timer = 180;
        private void Start()
        {
            StartCoroutine(Timer());
        }

        public IEnumerator Timer()
        {
            while (_timer > 0)
            {
                _textTimer.SetText(Convert((int)_timer));
                _timer -= 1;
                yield return new WaitForSeconds(1f);
            }
            if (_spawnHero._listHero.Count > 0)
            {
                _panel.gameObject.SetActive(true);
                _textNickName.SetText($"{_nickname[Random.Range(0, _nickname.Length - 1)]}");
            }
            _timer = 180;        
        }
        public string Convert(int time)
        {
            int minutes = (int)((time % 3600) / 60);
            int seconds = (int)(time % 60);

            // Форматирование строки
            return string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
        public void NextScene()
        {
            SceneManager.LoadScene(1);
        }
        public void Skip()
        {
            StartCoroutine(Timer());
            _panel.gameObject.SetActive(false);
            int tempMoney = (_mainUI._money * 20) / 100;
            int tempStrong = (_mainUI._totalStrong * 10) / 100;
            _mainUI.AddMoney(-tempMoney);
            _mainUI.AddStrong(-tempStrong);
        }
    }
}
