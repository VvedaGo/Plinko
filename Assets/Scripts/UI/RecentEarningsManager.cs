using Assets.Scripts.Game;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class RecentEarningsManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI[] textElements;

        [SerializeField] private Color red;
        [SerializeField] private Color green;
        [SerializeField] private Color grey;

        public void Initialize()
        {
            CollectionBasket.EarnedCoins += CollecitonCallback;

            foreach (TextMeshProUGUI text in textElements)
            {
                text.text = "";
            }
        }


        private void CollecitonCallback(object sender, CollectionBasket.OnBasketHit e)
        {
            AddNumber(e.Winnings, e.Factor);
        }


        private void AddNumber(float number, float factor)
        {
            for (int i = textElements.Length - 1; i > 0; i--)
            {
                textElements[i].color = textElements[i - 1].color;
                textElements[i].text = textElements[i - 1].text;
            }

            switch (factor)
            {
                case 1:
                {
                    textElements[0].color = Color.gray;
                    break;
                }
                case >1:
                {
                    textElements[0].color = green;
                    break;
                }
                case <1:
                {
                    textElements[0].color = red;
                    break;
                }
            }


            textElements[0].text = "+" + number.ToString("F3").Replace(",", ".");
        }
    }
}