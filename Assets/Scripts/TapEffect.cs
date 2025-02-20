using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TapEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        private void Click(Vector2 pos)
        {
            GameObject effect =  Instantiate(_prefab, pos, Quaternion.identity, transform);
            Destroy(effect, 0.4f);
        }
    }
}
