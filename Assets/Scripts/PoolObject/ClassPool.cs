using UnityEngine;

namespace Game
{
    public class ClassPool : Pool<MonoPool>
    {
        public ClassPool(int objects) : base(objects)
        {

        }
        public void DestroyPool(MonoBehaviour key)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].prefab.GetType() == key.GetType())
                {
                    Objects[i].DestroyPool();
                    Objects.Remove(Objects[i]);
                    return;
                }
            }
        }
        public MonoPool Create(MonoBehaviour prefab, Transform transform)
        {
            MonoPool mono = new MonoPool(prefab, transform, 1);
            Objects.Add(mono);
            return mono;
        }
        public MonoPool Create(MonoBehaviour prefab)
        {
            MonoPool mono = new MonoPool(prefab, null, 1);
            Objects.Add(mono);
            return mono;
        }
        public MonoPool Get(MonoBehaviour key, Transform transform = null)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].prefab.GetType() == key.GetType())
                {
                    return Objects[i];
                }
            }
            return Create(key, transform);
        }
    }
}
