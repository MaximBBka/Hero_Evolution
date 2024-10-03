using UnityEngine;

namespace Game
{
    public abstract class DragHandler : MonoBehaviour
    {
        private Vector3 pos2;
        private Vector2 areaPositive = new Vector2(5.5f, 3f);
        private Vector2 areaNegative = new Vector2(-8.3f, -1.8f);
        public virtual void OnMouseDrag()
        {
            Debug.Log("OnDrag");
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            pos2.z = 0;
            transform.position = pos + pos2;
            Vector3 currentPos = transform.position;
            currentPos.y = Mathf.Clamp(currentPos.y, areaNegative.y, areaPositive.y);
            currentPos.x = Mathf.Clamp(currentPos.x, areaNegative.x, areaPositive.x);
            transform.position = currentPos;
        }
        private void OnMouseDown()
        {
            pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos2.z = 0;
            pos2 = transform.position - pos2;
            OnStartDrag();
        }
        private void OnMouseUp()
        {
            OnEndDrag();
        }
        public virtual void OnStartDrag()
        {
            // логика первого нажатия
            Debug.Log("+1");
        }
        public virtual void OnEndDrag()
        {
            Debug.Log("OnEndDrag");
        }
    }
}
