using Zenject;
using UnityEngine;
using Game;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private MainUI _mainUI;
    [SerializeField] private WindowBook _windowBook;
    [SerializeField] private UIRecources _uiRes;
    [SerializeField] private WindowSelectHero _windowSelectHero;
    [SerializeField] private WindowAncoument _windowAncoument;
    [SerializeField] private WindowOpenHero _windowOpenHero;
    [SerializeField] private WindowAds _windowAds;
    public override void InstallBindings()
    {
        Container.BindInstance<MainUI>(_mainUI);
        Container.BindInstance<WindowBook>(_windowBook);
        Container.BindInstance<UIRecources>(_uiRes);
        Container.BindInstance<WindowSelectHero>(_windowSelectHero);
        Container.BindInstance<WindowAncoument>(_windowAncoument);
        Container.BindInstance<WindowOpenHero>(_windowOpenHero);
        Container.BindInstance<WindowAds>(_windowAds);
    }
}