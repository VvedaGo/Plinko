using Assets.Scripts.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinText;
        
        private CoinService _coinService;
        private float coins;

        [Inject]
        public void Construct(CoinService coinService)
        {
            _coinService = coinService;
    
            coinService.CoinChanged += UpdateCoinView;
            UpdateCoinView(coinService.GetCoins());
        }

        private void UpdateCoinView(float obj)
        {
            coinText.text = obj.ToString("F3").Replace(",", ".");
        }
    

    }
}