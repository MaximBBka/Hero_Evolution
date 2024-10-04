using Game;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private FillingEnemies _fillingEnemies;
    public override void InstallBindings()
    {
        Container.BindInstance<FillingEnemies>(_fillingEnemies);
    }
}