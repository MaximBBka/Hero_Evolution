using Game;
using UnityEngine;
using Zenject;

public class SpawnInstaller : MonoInstaller
{
    [SerializeField] private SpawnHero _spawnHero;

    public override void InstallBindings()
    {
        Container.BindInstance<SpawnHero>(_spawnHero);
    }
}