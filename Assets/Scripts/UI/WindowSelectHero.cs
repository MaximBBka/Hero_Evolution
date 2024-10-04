using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

namespace Game
{
    public class WindowSelectHero : MonoBehaviour
    {
        [field: SerializeField] public List<ModelHero> _selectedHero { get; private set; } = new List<ModelHero>(5);
        
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _textSelected;
        [SerializeField] private ElementBattleCharacter _prefab;
        [SerializeField] private List<int> _heroList;
        [SerializeField] private SOHero _hero;
        [SerializeField] private List<ElementBattleCharacter> _elementBattleCharacter = new List<ElementBattleCharacter>(5);
        [SerializeField] private Transform _buttonPlay;

        private int temp = 0;
        private void Start()
        {
            Load();
            Filled();
        }
        public void Load()
        {
            YandexGame.LoadProgress();
            _heroList = YandexGame.savesData.BaseHeroes;
        }
        private void Filled()
        {
            SortArray();
            for (int i = 0; i < _heroList.Count; i++)
            {
                ElementBattleCharacter element = Instantiate(_prefab, _content);
                element.Index = i;
                element.NumberHero.SetText($"{_heroList[i]}");
                element.ImageSelect.gameObject.SetActive(false);
                element.ImageHero.sprite = _hero.ModelHeroes[_heroList[i] - 1].Image;
                element.Hero = _hero.ModelHeroes[_heroList[i] - 1];
                element.OnSelect += Select;
                element.OnRemove += Remove;
            }
        }

        private void SortArray()
        {
            for (int i = 0; i < _heroList.Count; i++)
            {
                for (int j = 0; j < _heroList.Count; j++)
                {
                    if (_heroList[j] < _heroList[i])
                    {
                        int temp = _heroList[i];
                        _heroList[i] = _heroList[j];
                        _heroList[j] = temp;
                    }
                }
            }
        }
        private void Select(ModelHero hero, ElementBattleCharacter element)
        {
            if (_selectedHero.Count < 5)
            {
                _selectedHero.Add(hero);
                _elementBattleCharacter.Add(element);
                temp++;
                element.ImageSelect.gameObject.SetActive(true);
                element.IsSelect = false;
                UpdateText();
            }
        }
        private void Remove(ModelHero hero, ElementBattleCharacter element)
        {
            for (int i = 0; i < _selectedHero.Count; i++)
            {
                if (element.Index == _elementBattleCharacter[i].Index)
                {
                    _selectedHero.Remove(hero);
                    _elementBattleCharacter.Remove(element);
                    temp--;
                    element.ImageSelect.gameObject.SetActive(false);
                    element.IsSelect = true;
                    UpdateText();
                }
            }
        }
        private void UpdateText()
        {
            _textSelected.SetText($"Выбрано {temp}/5");
            if(temp > 0)
            {
                _buttonPlay.gameObject.SetActive(true );
            }else
            {
                _buttonPlay.gameObject.SetActive(false);
            }
        }
    }
}
