using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SpawnHero : MonoBehaviour
    {
        [SerializeField] private LayerManager _manager;
        [SerializeField] private Transform _spawnPool;
        [SerializeField] List<BaseHero> _listHero;

        private MainUI _mainUI;
        private ClassPool _pool;
        private ModelHero _currentModel;

        public SOHero sOHero;
        public int MaxHero;

        [Inject]
        public void Construct(MainUI ui)
        {
            _mainUI = ui;
        }

        public void Start()
        {
            SetHero();
            _pool = new ClassPool(1);
        }

        private void MergeUnit(BaseHero hero, BaseHero baseHero)
        {
            MonoPool mono = _pool.Get(hero);
            HeroUnSubcribe(hero);
            HeroUnSubcribe(baseHero);
            mono.PutAway(hero);
            mono.PutAway(baseHero);
            for (int i = 0; i < sOHero.ModelHeroes.Length; i++)
            {
                if (hero.GetType() == sOHero.ModelHeroes[i].Prefab.GetType())
                {
                    MonoPool newMono = _pool.Get(sOHero.ModelHeroes[i + 1].Prefab, _spawnPool);
                    BaseHero newUnit = newMono.Get() as BaseHero;
                    newUnit.Init(_manager, sOHero.ModelHeroes[i + 1]);
                    newUnit.transform.position = hero.transform.position;
                    HeroSubcribe(newUnit);
                    SetHero();
                }
            }
        }

        public void Spawn()
        {
            if (_listHero.Count < MaxHero && _mainUI._money >= _currentModel.Price)
            {
                _mainUI.AddMoney(-_currentModel.Price);
                Vector3 spawnPos = new Vector3(Random.Range(-8, 6), Random.Range(4.5f, -1.5f), 0);
                MonoPool mono = _pool.Get(_currentModel.Prefab, _spawnPool);
                BaseHero hero = mono.Get() as BaseHero;
                hero.transform.position = spawnPos;
                hero.Init(_manager, _currentModel);
                HeroSubcribe(hero);
                SetHero();
            }
        }
        public void SetHero()
        {
            for (int i = sOHero.ModelHeroes.Length - 1; i >= 0; i--)
            {
                if(_mainUI._totalStrong >= sOHero.ModelHeroes[i].NeedTotalStrong)
                {
                    BaseHero currentModel = _currentModel.Prefab;
                    _currentModel = sOHero.ModelHeroes[i];
                    if (currentModel == _currentModel.Prefab) return;
                    Replace(currentModel, sOHero.ModelHeroes[i].Prefab);
                    return;
                }         
            }
        }

        private void HeroSubcribe(BaseHero hero)
        {
            hero.OnMerge += MergeUnit;
            hero.OnMoneyChange += _mainUI.AddMoney;
            _listHero.Add(hero);
            CalculateStrong();
        }
        private void HeroUnSubcribe(BaseHero hero)
        {
            hero.OnMerge -= MergeUnit;
            hero.OnMoneyChange -= _mainUI.AddMoney;
            _listHero.Remove(hero);
        }
        private void CalculateStrong()
        {
            int temp = 0;
            for (int i = 0; i < _listHero.Count; i++)
            {
                temp += _listHero[i].Model.Strong;
            }
            _mainUI.UpdateStrong(temp);
        }

        private void Replace(BaseHero currentHero, BaseHero nextHero)
        {
            if(currentHero == null) 
            {
                return; 
            }
            MonoPool mono = _pool.Get(currentHero);
            List<Vector3> vector3 = new List<Vector3>(mono.Objects.Count);
            for (int i = 0; i < mono.Objects.Count; i++)
            {
                if (mono.Objects[i].gameObject.activeSelf) 
                {
                    vector3.Add(mono.Objects[i].transform.position);
                    HeroUnSubcribe(mono.Objects[i] as BaseHero);
                }
            }
            _pool.DestroyPool(currentHero);

            mono = _pool.Get(nextHero, _spawnPool);

            for (int i = 0; i < vector3.Count; i++)
            {
                BaseHero hero = mono.Get() as BaseHero;
                hero.transform.position = vector3[i];
                hero.Init(_manager, _currentModel);
                HeroSubcribe(hero);
            }
        }
    }
}
