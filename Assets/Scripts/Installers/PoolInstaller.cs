using Game;
using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance<ClassPool>(new ClassPool(1));
    }
}