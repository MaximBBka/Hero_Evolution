using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game
{
    public class WindowAds : MonoBehaviour
    {
        [SerializeField] private Transform _panel;
        [SerializeField] private TextMeshProUGUI _totalMoney;
        [SerializeField] private SOReward _reward;

        private MainUI _mainUI;
        private int _randomMoney;


        [Inject]
        public void Construct(MainUI ui)
        {
            _mainUI = ui;
        }

        private void Start()
        {
            StartCoroutine(RandomPos());
        }

        private IEnumerator RandomPos()
        {
            while (true)
            {
                _panel.gameObject.SetActive(false);
                yield return new WaitForSeconds(60);
                _panel.gameObject.SetActive(true);               
                SetRandomMoney();
                if (_randomMoney > 1000)
                {
                    int thousands = _randomMoney / 1000;
                    int hundreds = (_randomMoney % 1000) / 100;
                    _totalMoney.text = $"{thousands}.{hundreds}K";
                }
                else
                {
                    _totalMoney.text = $"{_randomMoney}";
                }
                yield return new WaitForSeconds(20);
                _panel.gameObject.SetActive(false);
            }
        }
        private void SetRandomMoney()
        {
            for (int i = _reward.Rewards.Length - 1; i >= 0; i--)
            {
                if (_mainUI._totalStrong >= _reward.Rewards[i].Strong)
                {
                    _randomMoney = Random.Range(_reward.Rewards[i].AddMoney, _reward.Rewards[i].AddMoney * 2);
                    return;
                }
            }
        }
        public void AddMoneyAds()
        {
            _mainUI.AddMoney(_randomMoney);
            _panel.gameObject.SetActive(false);
        }
    }
}
