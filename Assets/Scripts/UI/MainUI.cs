using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MainUI : MonoBehaviour
    {
        [field: SerializeField] public int _money { get; private set; } = 40;
        [field: SerializeField] public int _totalStrong { get; private set; }
        [field: SerializeField] public Slider _upMoney { get; private set; }

        

        [SerializeField] private TextMeshProUGUI _textMoney;
        [SerializeField] private TextMeshProUGUI _textStrong;


        private int _totalUpMoney = 4;
        private float _speedSliderDown = 0.1f;

        public int MultiplyMoney;
        public Image ImagePrice;
        public Image ImageAds;
        public TextMeshProUGUI TextPrice;

        private void Start()
        {
            StartCoroutine(SliderDown());
        }

        public void AddMoney(int money)
        {
            _money += money * MultiplyMoney;
            _textMoney.SetText($"{_money}");
        }
        public void UpdateStrong(int strong)
        {
            _totalStrong = strong;
            _textStrong.SetText($"{_totalStrong}");
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
    }
}
