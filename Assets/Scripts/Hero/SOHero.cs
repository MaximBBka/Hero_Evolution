using UnityEngine;
using System;

namespace Game
{
    [CreateAssetMenu(menuName = "Data/Game/Heroes")]
    public class SOHero : ScriptableObject
    {
        public ModelHero[] ModelHeroes;
    }

    [Serializable]
    public struct ModelHero
    {
        public BaseHero Prefab;
        public Sprite Image;
        public float Heath;
        public float Damage;
        public int Strong;
        public int Price;
        public int GetMoney;
        public int GetRes;
        public int NeedTotalStrong;
        public int Index;
    }
}