using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] private GameObject BulletPoolerPrefab;
    [SerializeField] private GameObject PlantPoolerPrefab;
    [SerializeField] private GameObject SunPoolerPrefab;
    [SerializeField] private GameObject ZoombiePoolerPrefab;


    public override void InstallBindings()
    {
        Container.Bind<PlantPooler>().FromComponentInNewPrefab(PlantPoolerPrefab).AsSingle();
        Container.Bind<SunPooler>().FromComponentInNewPrefab(SunPoolerPrefab).AsSingle();
        Container.Bind<ZoombiePooler>().FromComponentInNewPrefab(ZoombiePoolerPrefab).AsSingle();
        Container.Bind<BulletPooler>().FromComponentInNewPrefab(BulletPoolerPrefab).AsSingle();

        Container.Bind<IJumpHandler>().To<JumpHandler>().AsTransient();

        Container.Bind<IBaseAnimationHandler>().WithId("ShootingTree").To<ShootingTreeAnimationHandler>().AsTransient();
        Container.Bind<IBaseAnimationHandler>().WithId("EatingTree").To<EatingTreeAnimationHandler>().AsTransient();
        Container.Bind<IBaseAnimationHandler>().WithId("Sunflower").To<SunflowerAnimationHandler>().AsTransient();
    }
}
