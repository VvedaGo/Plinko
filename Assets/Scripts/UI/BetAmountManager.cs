using System;
using Assets.Scripts.Game;
using Assets.Scripts.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class BetAmountManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI betAmountText;
        private BallSpawner _ballSpawner;
        private CoinService _coinService;

        private float _betAmount;


        [Inject]
        public void Construct(BallSpawner ballSpawner, CoinService coinService)
        {
            _ballSpawner = ballSpawner;
            _coinService = coinService;
        }

        void Start()
        {
            _betAmount = Math.Min(5f, _coinService.GetCoins());

            if (_betAmount < 0.01)
            {
                _betAmount = 0.01f;
            }

            betAmountText.text = "DROP\n" + _betAmount.ToString("F2").Replace(",", ".");
        }

        public void AdditionOfOne()
        {
            _betAmount = Math.Min(_betAmount + 1, _coinService.GetCoins());
            if (_betAmount < 0.01)
            {
                _betAmount = 0.01f;
            }

            betAmountText.text = "DROP\n" + _betAmount.ToString("F2").Replace(",", ".");
        }


        public void MinusOfOne()
        {
            _betAmount = Math.Min(_betAmount - 1, _coinService.GetCoins());
            if (_betAmount < 0.01)
            {
                _betAmount = 0.01f;
            }

            betAmountText.text = "DROP\n" + _betAmount.ToString("F2").Replace(",", ".");
        }

        public void Duplication()
        {
            _betAmount = Math.Min(_betAmount * 2, _coinService.GetCoins());
            if (_betAmount < 0.01)
            {
                _betAmount = 0.01f;
            }

            betAmountText.text = "DROP\n" + _betAmount.ToString("F2").Replace(",", ".");
        }

        public void Division()
        {
            _betAmount = Math.Min(_betAmount / 2, _coinService.GetCoins());
            if (_betAmount < 0.01)
            {
                _betAmount = 0.01f;
            }

            betAmountText.text = "DROP\n" + _betAmount.ToString("F2").Replace(",", ".");
        }


        public void PlaceBet()
        {
            if (_coinService.GetCoins() >= _betAmount)
            {
                _coinService.UseCoins(_betAmount);
                _ballSpawner.SpawnBall(_betAmount);

            }
        }

        public float GetBetAmount()
        {
            return _betAmount;
        }
    }
}