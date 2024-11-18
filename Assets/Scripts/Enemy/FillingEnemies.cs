using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

namespace Game
{
    public class FillingEnemies : MonoBehaviour
    {
        [field: SerializeField] public List<ModelHero> _baseEnemy { get; private set; } = new List<ModelHero>(5);
        [SerializeField] private SOHero _hero;

        private int _totalBattle = 0;
        private int _battleWin = 0;
        private WindowSelectHero _windowSelectHero;

        public bool IsWin = true;

        [Inject]
        public void Construct(WindowSelectHero selectHero)
        {
            _windowSelectHero = selectHero;
        }
        private void Start()
        {
            Load();
        }
        public void Load()
        {
            YandexGame.LoadProgress();
            _totalBattle = YandexGame.savesData.TotalBattle;
            _battleWin = YandexGame.savesData.BattleWin;
        }
        public void Save()
        {
            YandexGame.savesData.TotalBattle = _totalBattle;
            YandexGame.savesData.BattleWin = _battleWin;
            YandexGame.SaveProgress();
        }
        private void BattleWin(int MaxIndex, int tempCount)
        {
            for (int i = 0; i < tempCount; i++)
            {
                if (_windowSelectHero._selectedHero[MaxIndex].Index > 1)
                {
                    _baseEnemy.Add(_hero.ModelHeroes[Random.Range(0, _windowSelectHero._selectedHero[MaxIndex].Index - 2)]);
                }
                else
                {
                    _baseEnemy.Add(_hero.ModelHeroes[0]);
                }
            }
        }
        private void BattleLose(int MaxIndex)
        {
            int tempCount = Random.Range(3, 6);
            for (int i = 0; i < tempCount; i++)
            {
                if (_windowSelectHero._selectedHero[MaxIndex].Index != _windowSelectHero._selectedHero.Count + 1 && _windowSelectHero._selectedHero[MaxIndex].Index != _windowSelectHero._selectedHero.Count - 1)
                {
                    _baseEnemy.Add(_hero.ModelHeroes[Random.Range(_windowSelectHero._selectedHero[MaxIndex].Index + 1, _windowSelectHero._selectedHero.Count - 1)]);
                }
                else
                {
                    _baseEnemy.Add(_hero.ModelHeroes[_windowSelectHero._selectedHero.Count - 1]);
                }
            }
        }

        public void LogicEnemy()
        {
            int tempCount = Random.Range(1, 6);
            int MaxIndex = 0;
            int IndexHero = 0;
            for(int i = 0; i < _windowSelectHero._selectedHero.Count; i++) 
            {
                if (_windowSelectHero._selectedHero[i].Index > IndexHero)
                {
                    IndexHero = _windowSelectHero._selectedHero[i].Index;
                    MaxIndex = i;
                }
            }
            if (_totalBattle < 3)
            {
                BattleWin(MaxIndex, tempCount);
                IsWin = true;
                _totalBattle++;
                Save();
                return;
            }
            else
            {
                if (_battleWin < 2)
                {
                    BattleWin(MaxIndex, tempCount);
                    IsWin = true;
                    _battleWin++;
                }else
                {
                    BattleLose(MaxIndex);
                    IsWin = false;
                    int temp = Random.Range(0, 4);
                    if(temp == 1)
                    {
                        _battleWin = 2;
                    }else
                    {
                        _battleWin = 0;
                    }                   
                }                
            }
            Save();
        }
        private void OnDestroy()
        {
            Save();
        }
    }
}
