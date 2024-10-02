

namespace Game
{
    public interface IMoney
    {
        public delegate void CallbackMoney(int money);
        public event CallbackMoney OnMoneyChange;
    }
}
