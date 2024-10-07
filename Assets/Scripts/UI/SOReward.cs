using UnityEngine;
using System;

namespace Game
{
    [CreateAssetMenu(menuName = "Data/Game/Rewards")]
    public class SOReward : ScriptableObject
    {
        public Reward[] Rewards;
    }

    [Serializable]
    public struct Reward
    {
        public int Strong;
        public int AddMoney;
        public int AddStrong;
        public int RemoveStrong;
    }
}
