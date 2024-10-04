using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game
{
    public class FillingEnemies : MonoBehaviour
    {
        [field: SerializeField] public List<ModelHero> _baseEnemy { get; private set; } = new List<ModelHero>(5);
        [SerializeField] private SOEnemy _enemy;
        [SerializeField] private SOHero _hero;

        private int _strongPlayer;
        private int _totalBattle = 0;
        private ModelEnemy _currentModelEnemy;
        private void Start()
        {
            Load();
            LogicEnemy();
            Fiiling();
        }
        public void Load()
        {
            YandexGame.LoadProgress();
            _strongPlayer = YandexGame.savesData.Strong;
        }
        public void Save()
        {
            YandexGame.savesData.TotalBattle = _totalBattle;
            YandexGame.SaveProgress();
        }
        public void LogicEnemy()
        {
            for (int i = 0; i < _enemy.ModelsEnemy.Length; i++)
            {
                if (_strongPlayer >= _enemy.ModelsEnemy[i].Strong)
                {
                    _currentModelEnemy = _enemy.ModelsEnemy[i];
                    _totalBattle++;
                    return;
                }
            }
        }
        public void Fiiling()
        {
            for (int i = 0; i < 5; i++)
            {
                _baseEnemy.Add(_hero.ModelHeroes[Random.Range(_currentModelEnemy.MinUnit, _currentModelEnemy.MaxUnit)]);
            }
        }
        private void OnDestroy()
        {
            Save();
        }
    }
}
