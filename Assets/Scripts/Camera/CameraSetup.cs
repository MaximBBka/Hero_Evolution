using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] Vector2 Offset;

        public void Update()
        {
            // �������� ������� ����������� ������ ������
            float targetAspect = Offset.x / Offset.y; // ������� ����������� ������
            float windowAspect = (float)Screen.width / (float)Screen.height;
            float scaleHeight = windowAspect / targetAspect;

            // ����������� ������ � ��������� ������� ��������� ������
            Rect rect = cam.rect;

            if (scaleHeight < 1.0f)
            {
                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;
            }
            else
            {
                float scaleWidth = 2.0f / scaleHeight;
                rect.width = scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidth) / 2.0f;
                rect.y = 0;
            }
            cam.rect = rect;
        }
    }
}
