using Assets.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    public class BallSpawner
    {
        private IPool<BallScript> _pool;
        private Vector2 _spawnPosition;
        private ScreenSizeManager _screenSizeManager;
        private float _increment;

        [Inject]
        public void Construct(IPool<BallScript> pool, ScreenSizeManager screenSizeManager)
        {
            _pool = pool;
            _screenSizeManager = screenSizeManager;
        }


        public void SpawnBall(float betAmount)
        {
            GameObject ballSpawned = _pool.GetFreeElement().gameObject;
            ballSpawned.transform.localPosition = _spawnPosition;
            ballSpawned.TryGetComponent(out BallScript ballScript);
            ballScript.Construct(_screenSizeManager);
            ballScript.Setup(_increment, betAmount);
        }

        public void AssignIncrement(float f)
        {
            _increment = f;
        }

        public void SetSpawnPosition(Vector2 vector2)
        {
            _spawnPosition = vector2;
        }
    }
}