
namespace Game
{
    public interface IDeath
    {
        public delegate void CallBackDeath(BaseHero baseHero);
        public event CallBackDeath OnDeath;
    }
}
