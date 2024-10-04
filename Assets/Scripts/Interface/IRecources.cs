
using UnityEngine;

namespace Game
{
    public interface IRecources 
    {
        public delegate void CallBackRes(int total, Transform pos);
        public event CallBackRes OnAddRes;
    }
}
