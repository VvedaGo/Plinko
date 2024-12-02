using Assets.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
   public class BuyCoinPanel : MonoBehaviour
   {
      private CoinService _coinService;
      private int _amountBuy;

      [Inject]
      public void Construct(CoinService coinService)
      {
         _coinService = coinService;
      }

      public void Open()
      {
         gameObject.SetActive(true);  
      }

      public void Close()
      {
         gameObject.SetActive(false);
      }

      public void BuyCoin()
      {
         _amountBuy = 100;
         _coinService.GainedCoins(_amountBuy);
      }
   }
}
