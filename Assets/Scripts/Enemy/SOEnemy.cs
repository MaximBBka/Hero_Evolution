using UnityEngine;
using System;

namespace Game
{
    [CreateAssetMenu(menuName = "Data/Game/Enemy")]
    public class SOEnemy : ScriptableObject
    {
        public ModelEnemy[] ModelsEnemy;
    }
    [Serializable]
    public struct ModelEnemy
    {
        public int Strong;
        public int MaxUnit;
        public int MinUnit;
    }
}
