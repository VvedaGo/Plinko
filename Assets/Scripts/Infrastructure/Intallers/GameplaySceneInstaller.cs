using Assets.Scripts.Game;
using Assets.Scripts.Services;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Intallers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private BallScript _prefabBall;
        [SerializeField] private Transform _container;
        [SerializeField] private CoinManager _coinManager;
        [SerializeField] private BallPlacementManager _ballPlacementManager;
        [SerializeField] private BuyCoinPanel _buyCoinPanel;
        public override void InstallBindings()
        {
           
            Container.Bind<BallSpawner>().AsSingle();
            Container.Bind<ScreenSizeManager>().AsSingle();
            Container.Bind<CoinService>().AsSingle().NonLazy();

            Container
                .Bind<IPool<BallScript>>()
                .To<PoolMono<BallScript>>()
                .AsSingle()
                .WithArguments(_prefabBall, 10, _container).NonLazy();
            
            Container.Bind<BuyCoinPanel>().FromComponentInHierarchy().AsSingle();
                // Container.Inject(_buyCoinPanel);
            Container.Inject(_coinManager);
            Container.Inject(_ballPlacementManager);
           
        }
    }
}