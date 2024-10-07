using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class WindowBook : MonoBehaviour
    {
        [SerializeField] private ElementBook _prefabUI;
        [SerializeField] private SOHero _soHero;
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _strong;
        [SerializeField] private Image _titleImage;

        private List<ElementBook> _arrayElement = new List<ElementBook>();

        public void OnStart()
        {
            Filled();
            ShowCharacter(0);
        }
        public void Filled()
        {
            for (int i = 0; i < _soHero.ModelHeroes.Length; i++)
            {
                ElementBook element = Instantiate(_prefabUI, _content);
                element.ImageCharacter.sprite = _soHero.ModelHeroes[i].Image;
                element.ImageCharacter.color = Color.black;
                element.TextNumber.SetText($"{i + 1}");
                element.Index = i;
                _arrayElement.Add(element);
            }
            Load();
        }

        public void UpdateCharacterInfo(int index)
        {
            _titleImage.sprite = _soHero.ModelHeroes[index].Image;
            _health.SetText($"{_soHero.ModelHeroes[index].Heath}");
            _damage.SetText($"{_soHero.ModelHeroes[index].Damage}");
            _strong.SetText($"{_soHero.ModelHeroes[index].Strong}");
        }

        public void ShowCharacter(int index)
        {
            _arrayElement[index].IsVisibale = true;
            _arrayElement[index].ImageCharacter.color = Color.white;
            if (!YandexGame.savesData.ElementBookOpen.Contains(index))
            {
                YandexGame.savesData.ElementBookOpen.Add(index);
                YandexGame.SaveProgress();
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < _arrayElement.Count; i++)
            {
                _arrayElement[i].OncallbackElement += UpdateCharacterInfo;
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < _arrayElement.Count; i++)
            {
                _arrayElement[i].OncallbackElement -= UpdateCharacterInfo;
            }
        }
        private void OnDestroy()
        {
            for (int i = 0; i < _arrayElement.Count; i++)
            {
                Destroy(_arrayElement[i].gameObject);
            }
        }
        private void Load()
        {
            YandexGame.LoadProgress();
            List<int> temp = YandexGame.savesData.ElementBookOpen;

            for (int i = 0; i < temp.Count; i++)
            {
                _arrayElement[temp[i]].IsVisibale = true;
                _arrayElement[temp[i]].ImageCharacter.color = Color.white;
            }
        }
    }
}
