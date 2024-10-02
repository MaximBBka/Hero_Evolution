using UnityEngine;

namespace Game
{
    public class LayerManager : MonoBehaviour
    {
        [SerializeField] private Transform _upPoint;
        [SerializeField] private Transform _downPoint;
        private int Layers = 20;
        private float step;

        private void Start()
        {
            step = (_upPoint.transform.position.y - _downPoint.transform.position.y) / (Layers);
        }
        public float SetLayer(float height)
        {
            for (int i = 0; i < Layers; i++)
            {
                float layerHeight = _downPoint.transform.position.y + (step * i);
                if (height <= layerHeight)
                {
                    return layerHeight;
                }
            }
            return Layers;
        }
    }
}
