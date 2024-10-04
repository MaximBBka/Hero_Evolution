using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ElementBattleCharacter : MonoBehaviour
    {
        [field:SerializeField] public Image ImageHero {  get; private set; }
        [field:SerializeField] public Image ImageSelect {  get; private set; }
        [field:SerializeField] public TextMeshProUGUI NumberHero {  get; private set; }

        public bool IsSelect = true;
        public int Index;
        public ModelHero Hero;
        public delegate void CallBackSelect(ModelHero hero, ElementBattleCharacter element);
        public event CallBackSelect OnSelect;
        public event CallBackSelect OnRemove;

        public void OnClick()
        {
            if (IsSelect)
            {
                OnSelect.Invoke(Hero, this);
            }
            else
            {
                OnRemove.Invoke(Hero, this);
            }        
        }
    }
}
