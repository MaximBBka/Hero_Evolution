using Zenject;
using UnityEngine;
using Game;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private MainUI _mainUI;
    public override void InstallBindings()
    {
        Container.BindInstance<MainUI>(_mainUI);
    }
}