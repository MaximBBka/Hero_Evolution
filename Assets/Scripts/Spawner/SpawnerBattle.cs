using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SpawnerBattle : MonoBehaviour
    {
        [SerializeField] private Transform[] SpawnPosHero;
        [SerializeField] private Transform[] SpawnPosEnemy;

        private WindowSelectHero _windowSelectHero;
        private FillingEnemies _fillingEnemies;

        [Inject]
        public void Construct(WindowSelectHero selectHero, FillingEnemies filling)
        {
            _windowSelectHero = selectHero;
            _fillingEnemies = filling;
        }

        public void SpawmHero()
        {
            for (int i = 0; i < _windowSelectHero._selectedHero.Count; i++)
            {
                BaseHero baseHero = Instantiate(_windowSelectHero._selectedHero[i].Prefab, SpawnPosHero[i]);
                baseHero.Init(_windowSelectHero._selectedHero[i]);
                baseHero.IsBattle = true;
                baseHero.Render.sortingOrder = i;
            }
        }
        public void SpawnEnemy()
        {
            for (int i = 0; i < _fillingEnemies._baseEnemy.Count; i++)
            {
                BaseHero baseEnemy = Instantiate(_fillingEnemies._baseEnemy[i].Prefab, SpawnPosEnemy[i]);
                baseEnemy.Init(_fillingEnemies._baseEnemy[i]);
                baseEnemy.IsBattle = true;
                baseEnemy.Render.flipX = true;
                baseEnemy.Render.sortingOrder = i;
            }
        }
    }
}
