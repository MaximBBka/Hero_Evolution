using UnityEngine;

namespace Game
{
    public abstract class BaseResource : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        private void OnDisable()
        {
            SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 1f);
        }
    }
}
