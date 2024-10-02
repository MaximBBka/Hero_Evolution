using System.Collections.Generic;

namespace Game
{
    public abstract class Pool<T>
    {
        public List<T> Objects;
        public Pool(int objects)
        {
            Objects = new List<T>(objects);
        }
    }
    public abstract class CustomPool<T> : Pool<T>
    {
        protected CustomPool(int objects) : base(objects)
        {
        }
        public abstract void DestroyPool();
        public abstract T Get();
        public abstract T Create();
        public abstract void PutAway(T obj);
    }
}
