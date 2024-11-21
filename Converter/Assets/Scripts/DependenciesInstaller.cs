using UnityEngine;
using Zenject;

public class DependenciesInstaller : MonoInstaller
{
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private CurrencyCalculator _calculator;
    public override void InstallBindings()
    {
        Container.Bind<ViewManager>().FromInstance(_viewManager).AsSingle().NonLazy();
        Container.Bind<CurrencyCalculator>().FromInstance(_calculator).AsSingle().NonLazy();
    }
}
