using System;
using Assets.Scripts.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    public class CollectionBasket : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI multiplierText;
        [SerializeField] private SpriteRenderer main;

        public static event EventHandler<OnBasketHit> EarnedCoins;

        public class OnBasketHit : EventArgs
        {
            public float Winnings;
            public float Factor;
        }


        private CoinService _coinService;
        private float _multiplier;

        [Inject]
        public void Construct(CoinService coinService)
        {
            _coinService = coinService;
        }


        public void Setup(float multiplier, Color color)
        {
            _multiplier = multiplier;

            multiplierText.text = multiplier.ToString("F1").Replace(",", ".");
            if (color != Color.green)
            {
                main.color = color;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            _coinService.GainedCoins(collision2D.gameObject.GetComponent<BallScript>().GetBetValue() * _multiplier);

            EarnedCoins?.Invoke(this,
                new OnBasketHit
                {
                    Winnings = collision2D.gameObject.GetComponent<BallScript>().GetBetValue() * _multiplier,
                    Factor = _multiplier
                });

            collision2D.gameObject.GetComponent<BallScript>().ResetState();
        }
    }
}