using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ElementBook : MonoBehaviour
    {
        [field: SerializeField] public Image ImageCharacter { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TextNumber { get; private set; }

        public delegate void CallbackElement(int index);
        public event CallbackElement OncallbackElement;

        public int Index;
        public bool IsVisibale;

        public void OnCLick()
        {
            if (IsVisibale)
            {
                OncallbackElement.Invoke(Index);
            }
        }
    }
}
