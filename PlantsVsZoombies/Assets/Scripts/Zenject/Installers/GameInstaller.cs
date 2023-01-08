using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameSystem>().AsSingle().NonLazy();
        Container.Bind<SoundSystem>().AsSingle().NonLazy();
        Container.Bind<MapSystem>().AsSingle();
        Container.Bind<PlantSystem>().AsSingle();
        Container.Bind<ZoombieSystem>().AsSingle();
        Container.Bind<SunSystem>().AsSingle();
    }
}
