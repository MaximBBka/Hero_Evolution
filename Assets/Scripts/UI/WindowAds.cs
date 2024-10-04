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

        public IEnumerator RandomPos()
        {
            while (true)
            {
                _panel.gameObject.SetActive(false);
                yield return new WaitForSeconds(60);
                _panel.gameObject.SetActive(true);
                _randomMoney = Random.Range(500, 1500);
                _totalMoney.SetText($"{_randomMoney}");
                yield return new WaitForSeconds(20);
                _panel.gameObject.SetActive(false);
            }
        }

        public void AddMoneyAds()
        {
            _mainUI.AddMoney(_randomMoney);
            _panel.gameObject.SetActive(false);
        }
    }
}
