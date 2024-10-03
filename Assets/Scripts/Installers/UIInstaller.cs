using Zenject;
using UnityEngine;
using Game;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private MainUI _mainUI;
    [SerializeField] private WindowBook _windowBook;
    public override void InstallBindings()
    {
        Container.BindInstance<MainUI>(_mainUI);
        Container.BindInstance<WindowBook>(_windowBook);
    }
}