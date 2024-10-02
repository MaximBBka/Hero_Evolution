using System.Linq;
using UnityEngine;

namespace Game
{
    public class MonoPool: CustomPool<MonoBehaviour> 
    {
        public MonoBehaviour prefab;
        [SerializeField] private Transform container;
        
        public MonoPool(MonoBehaviour _prefab, Transform _container, int countObjects) : base(countObjects)
        {
            prefab = _prefab;
            container = _container;
            for (int i = 0; i < countObjects; i++)
            {
                var obj = GameObject.Instantiate(_prefab, _container);
                obj.gameObject.SetActive(false);
                Objects.Add(obj);
            }
        }
        public MonoPool(MonoBehaviour _prefab, int countObjects) : base(countObjects)
        {
            prefab = _prefab;

            for (int i = 0; i < countObjects; i++)
            {
                var obj = GameObject.Instantiate(_prefab);
                obj.gameObject.SetActive(false);
                Objects.Add(obj);
            }
        }

        public override MonoBehaviour Get()
        {
            var obj = Objects.FirstOrDefault(obje => !obje.gameObject.activeSelf);

            if (obj == null)
            {
                obj = Create();
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public override void PutAway(MonoBehaviour obj)
        {
            obj.gameObject.SetActive(false);
        }
        public override MonoBehaviour Create()
        {
            var obj = GameObject.Instantiate(prefab, container);
            Objects.Add(obj);
            return obj;
        }

        public void PutAway(MonoBehaviour obj, Transform container)
        {
            PutAway(obj);
            obj.gameObject.transform.position = container.position;
        }
        public override void DestroyPool()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                GameObject.Destroy(Objects[i].gameObject);
            }
            Objects.Clear();
        }
    }
}
