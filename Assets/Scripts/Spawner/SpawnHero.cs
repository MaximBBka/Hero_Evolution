using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

namespace Game
{
    public class SpawnHero : MonoBehaviour
    {
        [field: SerializeField] public List<BaseHero> _listHero {  get; private set; }
        [SerializeField] private LayerManager _manager;
        [SerializeField] private Transform _spawnPool;

        private MainUI _mainUI;
        private WindowBook _windowBook;
        private UIRecources _uiResources;
        private WindowOpenHero _windowOpenHero;
        private ClassPool _pool;
        private ModelHero _currentModel;
        private ModelHero _AdsModel;

        public SOHero sOHero;
        public int MaxHero;

        private int _totalSpawnUnit;
        private Sprite _currentSpawn;
        private Sprite _adsSpawn;
        private int _saveHero;

        [Inject]
        public void Construct(MainUI ui, WindowBook window, UIRecources recources, ClassPool pool, WindowOpenHero openHero)
        {
            _mainUI = ui;
            _windowBook = window;
            _uiResources = recources;
            _pool = pool;
            _windowOpenHero = openHero;
        }
        public void Awake()
        {
            Load();
        }
        private void Start()
        {
            StartSpawn();
            SetHero();
            _windowBook.OnStart();
        }

        private void Load()
        {
            YandexGame.LoadProgress();
            _totalSpawnUnit = YandexGame.savesData.TotalSpawnUnits;
            _currentSpawn = YandexGame.savesData.CurrentSpawn;
            _adsSpawn = YandexGame.savesData.AdsSpawn;
            _saveHero = YandexGame.savesData.BaseHeroes.Count;
        }
        private void StartSpawn()
        {
            for (int i = 0; i < _saveHero; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8.3f, 5.5f), Random.Range(3f, -1.8f), 0);
                MonoPool mono = _pool.Get(sOHero.ModelHeroes[YandexGame.savesData.BaseHeroes[i] - 1].Prefab, _spawnPool);
                BaseHero hero = mono.Get() as BaseHero;
                hero.transform.position = spawnPos;
                hero.Init(sOHero.ModelHeroes[YandexGame.savesData.BaseHeroes[i] - 1], _manager);
                HeroSubcribe(hero);
                SetLayer(hero);
            }
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
                    AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Merge[Random.Range(0, AudioManager.Instance.Merge.Length - 1 + 1)]);
                    MonoPool newMono = _pool.Get(sOHero.ModelHeroes[i + 1].Prefab, _spawnPool);
                    BaseHero newUnit = newMono.Get() as BaseHero;
                    newUnit.Init(sOHero.ModelHeroes[i + 1], _manager);
                    newUnit.transform.position = hero.transform.position;
                    _windowBook.ShowCharacter(i + 1);
                    if(newUnit.CurrentIndex > _windowOpenHero.IndexOpen)
                    {
                        _windowOpenHero.StartOpen(newUnit.Model.Image);
                        _windowOpenHero.IndexOpen = newUnit.CurrentIndex;
                    }
                    HeroSubcribe(newUnit);
                    _totalSpawnUnit++;
                    SetHero();
                    return;
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
                hero.Init(_currentModel, _manager);
                HeroSubcribe(hero);
                SetLayer(hero);
                SetHero();
                _totalSpawnUnit++;
            }
        }
        public void SetHero()
        {
            for (int i = sOHero.ModelHeroes.Length - 1; i >= 0; i--)
            {
                if (_totalSpawnUnit >= sOHero.ModelHeroes[i].SpawnTotal)
                {
                    BaseHero currentModel = _currentModel.Prefab;
                    _currentModel = sOHero.ModelHeroes[i];
                    Save();
                    if (currentModel == _currentModel.Prefab) return;
                    Replace(currentModel, sOHero.ModelHeroes[i].Prefab);
                    _mainUI.ImagePrice.sprite = _currentModel.Image;
                    _currentSpawn = _currentModel.Image;
                    if (_currentModel.Price > 1000)
                    {
                        int thousands = _currentModel.Price / 1000;
                        int hundreds = (_currentModel.Price % 1000) / 100;
                        _mainUI.TextPrice.text = $"{thousands}.{hundreds}K";
                    }
                    else
                    {
                        _mainUI.TextPrice.text = $"{_currentModel.Price}";
                    }
                    if (_currentModel.Prefab == sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 1].Prefab)
                    {
                        _mainUI.ImageAds.sprite = _currentModel.Image;
                        _AdsModel = _currentModel;
                        _adsSpawn = _currentModel.Image;
                    }
                    else if (_currentModel.Prefab == sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 2].Prefab)
                    {
                        _mainUI.ImageAds.sprite = sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 1].Image;
                        _adsSpawn = sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 1].Image;
                        _AdsModel = sOHero.ModelHeroes[sOHero.ModelHeroes.Length - 1];
                    }
                    else
                    {
                        _mainUI.ImageAds.sprite = sOHero.ModelHeroes[i + 2].Image;
                        _adsSpawn = sOHero.ModelHeroes[i + 2].Image;
                        _AdsModel = sOHero.ModelHeroes[i + 2];
                    }
                    Save();
                    return;
                }
            }           
        }
        private void OnDestroy()
        {
            Save();
        }
        private void Save()
        {
            YandexGame.savesData.BaseHeroes.Clear();
            for (int i = 0; i < _listHero.Count; i++)
            {
                YandexGame.savesData.BaseHeroes.Add(_listHero[i].CurrentIndex);
            }
            YandexGame.savesData.TotalSpawnUnits = _totalSpawnUnit;
            YandexGame.savesData.CurrentSpawn = _currentSpawn;
            YandexGame.savesData.AdsSpawn = _adsSpawn;
            YandexGame.SaveProgress();
        }

        private void HeroSubcribe(BaseHero hero)
        {
            hero.OnMerge += MergeUnit;
            hero.OnMoneyChange += _mainUI.AddMoneyMultiply;
            hero.OnMoneyUp += _mainUI.UpMoney;
            hero.OnAddRes += _uiResources.AddRes;
            _listHero.Add(hero);
            _mainUI.AddStrong(hero.Model.Strong);
        }
        private void HeroUnSubcribe(BaseHero hero)
        {
            hero.OnMerge -= MergeUnit;
            hero.OnMoneyChange -= _mainUI.AddMoneyMultiply;
            hero.OnMoneyUp -= _mainUI.UpMoney;
            hero.OnAddRes -= _uiResources.AddRes;
            _listHero.Remove(hero);
            _mainUI.AddStrong(-hero.Model.Strong);
        }

        private void Replace(BaseHero currentHero, BaseHero nextHero)
        {
            if (currentHero == null)
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
                hero.Init(_currentModel, _manager);
                HeroSubcribe(hero);
            }
        }
        private void SetLayer(BaseHero hero)
        {
            Vector3 vector = new Vector3(hero.transform.position.x, hero.transform.position.y, hero.transform.position.z);
            vector.z = _manager.SetLayer(hero.transform.position.y) + Random.Range(0.0001f, 0.1111f);
            hero.transform.position = vector;
        }
        public void AdsSpawn()
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8.3f, 5.5f), Random.Range(3f, -1.8f), 0);
            MonoPool mono = _pool.Get(_AdsModel.Prefab, _spawnPool);
            BaseHero hero = mono.Get() as BaseHero;
            hero.transform.position = spawnPos;
            hero.Init(_AdsModel, _manager);
            HeroSubcribe(hero);
            SetLayer(hero);
            SetHero();
            _windowBook.ShowCharacter(hero.CurrentIndex - 1);
            _windowBook.ShowCharacter(hero.CurrentIndex - 2);
            if (hero.CurrentIndex > _windowOpenHero.IndexOpen)
            {
                _windowOpenHero.StartOpen(hero.Model.Image);
                _windowOpenHero.IndexOpen = hero.CurrentIndex;
            }
        }
    }
}
