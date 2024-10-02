using TMPro;
using UnityEngine;

namespace Game
{
    public class MainUI : MonoBehaviour
    {
        [field: SerializeField] public int _money { get; private set; } = 40;
        [field: SerializeField] public int _totalStrong {  get; private set; }
        
        [SerializeField] private TextMeshProUGUI _textMoney;
        [SerializeField] private TextMeshProUGUI _textStrong;

        public void AddMoney(int money)
        {
            _money += money;
            _textMoney.SetText($"{_money}");
        }
        public void UpdateStrong(int strong)
        {
            _totalStrong = strong;
            _textStrong.SetText($"{_totalStrong}");
        }
    }
}
