using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

namespace Game
{
    public class WindowBattle : MonoBehaviour
    {
        [SerializeField] private Transform _panel;
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textTimer;
        [SerializeField] private TextMeshProUGUI _textNickName;
        [SerializeField] private TextMeshProUGUI _textStrongEnemy;
        [SerializeField] private string[] _nickname;
        [SerializeField] private float _timer = 10;

        private MainUI _mainUI;
        private SpawnHero _spawnHero;
        private int _totalBattle;
        private int _battleWin;
        private string _enemyName;
        private int _enemyStrong;

        [Inject]
        public void Construct(MainUI ui, SpawnHero spawnHero)
        {
            _mainUI = ui;
            _spawnHero = spawnHero;
        }
        private void Start()
        {
            Load();
            StartCoroutine(Timer());
        }
        private void Load()
        {
            YandexGame.LoadProgress();
            _totalBattle = YandexGame.savesData.TotalBattle;
            _battleWin = YandexGame.savesData.BattleWin;
        }
        private void Save()
        {
            YandexGame.savesData.EnemyName = _enemyName;
            YandexGame.savesData.EnemyStrong = _enemyStrong;
            YandexGame.SaveProgress();
        }
        public IEnumerator Timer()
        {
            while (_timer > 0)
            {
                _textTimer.SetText(Convert((int)_timer));
                _timer -= 1;
                yield return new WaitForSeconds(1f);
            }
            _timer = 90;
            if (_spawnHero._listHero.Count > 0)
            {
                SetStrongEnemy();
                _panel.gameObject.SetActive(true);
                _enemyName = _nickname[Random.Range(0, _nickname.Length - 1)];
                _textNickName.SetText($"{_enemyName}");
                Save();
            }
            else
            {
                StopCoroutine(Timer());
                StartCoroutine(Timer());
            }
        }
        public string Convert(int time)
        {
            int minutes = (int)((time % 3600) / 60);
            int seconds = (int)(time % 60);

            // Форматирование строки
            return string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
        private void SetStrongEnemy()
        {
            if (_totalBattle < 3)
            {
                _enemyStrong = Random.Range(_mainUI._totalStrong < 50 ? 1 : 50, _mainUI._totalStrong > 50 ? _mainUI._totalStrong : 49);
                _textStrongEnemy.SetText($"{_enemyStrong}");
            }else
            {
                if(_battleWin < 2)
                {
                    _enemyStrong = Random.Range(_mainUI._totalStrong < 50 ? 1 : 50, _mainUI._totalStrong > 50 ? _mainUI._totalStrong : 49);
                    _textStrongEnemy.SetText($"{_enemyStrong}");
                }
                else
                {
                    _enemyStrong = Random.Range(_mainUI._totalStrong + 50, _mainUI._totalStrong + 400);
                    _textStrongEnemy.SetText($"{_enemyStrong}");
                }
            }
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
