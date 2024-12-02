using System;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services
{
    public class CoinService
    {
        [Inject] private BuyCoinPanel _buyCoinPanel;
        
        public event Action<float> CoinChanged;
        
        private float _coins;
        private double _coinValueToBuy = 1;

        public CoinService()
        {
            LoadData();
        }


        public void UseCoins(float amnt)
        {
            _coins -= amnt;
            CoinChanged?.Invoke(_coins);
            CheckCoinCount();
            SaveData();
        }

        private void CheckCoinCount()
        {
            if (_coins <= _coinValueToBuy)
            {
                _buyCoinPanel.Open();
            }
        }

        public void GainedCoins(float amnt)
        {
            _coins += amnt;

            CoinChanged?.Invoke(_coins);
            SaveData();
        }

        public float GetCoins()
        {
            return _coins;
        }

        void LoadData()
        {
            _coins = PlayerPrefs.GetFloat("coins", 1000);

            CoinChanged?.Invoke(_coins);
        }

        void SaveData()
        {
            PlayerPrefs.SetFloat("coins", _coins);
        }
    }
}