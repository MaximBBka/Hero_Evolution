

namespace Game
{
    public interface IMerge
    {
        public delegate void CallBackMerge(BaseHero baseUnit, BaseHero baseHero);
        public event CallBackMerge OnMerge;
    }
}
