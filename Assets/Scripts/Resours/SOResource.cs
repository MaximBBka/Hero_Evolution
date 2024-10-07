using UnityEngine;
using System;

namespace Game
{
    [CreateAssetMenu(menuName = "Data/Game/Recource")]
    public class SOResource : ScriptableObject
    {
        public ModelRecource[] _modelRecource;
    }

    [Serializable]
    public struct ModelRecource
    {
        public int Level;
        public int Price;
        public int BonusStrong;
        public int[] RandomResAds;
    }
}
