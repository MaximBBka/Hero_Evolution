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
        public void LogicEnemy()
        {
            if (_totalBattle < 3)
            {
                for (int i = 0; i < _windowSelectHero._selectedHero.Count; i++)
                {
                    if(_windowSelectHero._selectedHero[i].Index > 1)
                    {
                        _baseEnemy.Add(_hero.ModelHeroes[_windowSelectHero._selectedHero[i].Index - 2]);
                    }
                    else
                    {
                        _baseEnemy.Add(_hero.ModelHeroes[0]);
                    }                 
                }
                _totalBattle++;
                return;
            }
            else
            {
                if (_battleWin < 2)
                {
                    for (int i = 0; i < _windowSelectHero._selectedHero.Count; i++)
                    {
                        if (_windowSelectHero._selectedHero[i].Index > 1)
                        {
                            _baseEnemy.Add(_hero.ModelHeroes[Random.Range(0,_windowSelectHero._selectedHero[i].Index - 2)]);
                        }
                        else
                        {
                            _baseEnemy.Add(_hero.ModelHeroes[0]);
                        }
                    }                   
                    _battleWin++;
                }else
                {
                    for (int i = 0; i < _windowSelectHero._selectedHero.Count; i++)
                    {
                        if (_windowSelectHero._selectedHero[i].Index > 1)
                        {
                            if(_windowSelectHero._selectedHero[i].Index == _hero.ModelHeroes.Length - 1)
                            {
                                _baseEnemy.Add(_hero.ModelHeroes[_hero.ModelHeroes.Length - 1]);
                            }
                            else
                            {
                                _baseEnemy.Add(_hero.ModelHeroes[Random.Range(_windowSelectHero._selectedHero[i].Index, _hero.ModelHeroes.Length - 1)]);
                            }                     
                        }
                        else
                        {
                            _baseEnemy.Add(_hero.ModelHeroes[Random.Range(_windowSelectHero._selectedHero[i].Index, _hero.ModelHeroes.Length - 1)]);
                        }
                    }
                    int temp = Random.Range(0, 4);
                    if(temp == 1)
                    {
                        _battleWin = 2;
                    }else
                    {
                        _battleWin = 0;
                    }                   
                }
                Save();
            }                 
        }
        private void OnDestroy()
        {
            Save();
        }
    }
}
