using UI;
using UI.Interfaces;
using UI.Sealed;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public GameObject UiPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<UIDocument>().FromComponentInNewPrefab(UiPrefab).AsSingle().NonLazy();
        
        Container.Bind<ScreenController>().AsSingle().NonLazy();

        Container.Bind<IScreen>().WithId(ScreenName.Main.ToString()).To<MainMenu>().AsSingle().NonLazy();
        Container.Bind<IScreen>().WithId(ScreenName.Photo.ToString()).To<PhotoMenu>().AsSingle().NonLazy();
    }
}