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
        private WindowBook _windowBook;
        private ClassPool _pool;
        private ModelHero _currentModel;

        public SOHero sOHero;
        public int MaxHero;

        [Inject]
        public void Construct(MainUI ui, WindowBook window)
        {
            _mainUI = ui;
            _windowBook = window;
        }

        public void Start()
        {
            _currentModel = sOHero.ModelHeroes[0];
           // SetHero();
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
                    _windowBook.ShowCharacter(i + 1);
                    HeroSubcribe(newUnit);
                   // SetHero();
                }
            }
        }

        public void Spawn()
        {
            if (_listHero.Count < MaxHero && _mainUI._money >= _currentModel.Price)
            {
                _mainUI.AddMoney(-_currentModel.Price);
                Vector3 spawnPos = new Vector3(Random.Range(-8.3f, 5.5f), Random.Range(3f, -1.8f), 0);
                MonoPool mono = _pool.Get(_currentModel.Prefab, _spawnPool);
                BaseHero hero = mono.Get() as BaseHero;
                hero.transform.position = spawnPos;
                hero.Init(_manager, _currentModel);
                HeroSubcribe(hero);

                Vector3 vector = new Vector3(hero.transform.position.x, hero.transform.position.y, hero.transform.position.z);
                vector.z = _manager.SetLayer(hero.transform.position.y) + Random.Range(0.0001f, 0.1111f);
                hero.transform.position = vector;
                //SetHero();
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
                    _mainUI.ImagePrice.sprite = _currentModel.Image;
                    _mainUI.TextPrice.SetText($"{_currentModel.Price}");
                    if(_currentModel.GetType() == sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 1].GetType())
                    {
                        _mainUI.ImageAds.sprite = _currentModel.Image;
                    }else if(_currentModel.GetType() == sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 2].GetType())
                    {
                        _mainUI.ImageAds.sprite = sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 1].Image;
                    }else
                    {
                        _mainUI.ImageAds.sprite = sOHero.ModelHeroes[i + 2].Image;
                    }
                    return;
                }         
            }
        }

        private void HeroSubcribe(BaseHero hero)
        {
            hero.OnMerge += MergeUnit;
            hero.OnMoneyChange += _mainUI.AddMoney;
            hero.OnMoneyUp += _mainUI.UpMoney;
            _listHero.Add(hero);
            CalculateStrong();
        }
        private void HeroUnSubcribe(BaseHero hero)
        {
            hero.OnMerge -= MergeUnit;
            hero.OnMoneyChange -= _mainUI.AddMoney;
            hero.OnMoneyUp -= _mainUI.UpMoney;
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
