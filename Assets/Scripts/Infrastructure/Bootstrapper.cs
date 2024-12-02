using Assets.Scripts.Game;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private BallPlacementManager _ballPlacementManager;
        [SerializeField] private RecentEarningsManager _recentEarningsManager;

        private void Start()
        {
            _ballPlacementManager.GeneratePyramind(rows: 8);
            _recentEarningsManager.Initialize();
        }
    }
}