

namespace Game
{
    public interface IMoney
    {
        public delegate void CallbackMoney(int money);
        public event CallbackMoney OnMoneyChange;

        public delegate void CallbackUpMoney();
        public event CallbackUpMoney OnMoneyUp;
    }
}
