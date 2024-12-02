using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    public class BallPlacementManager : MonoBehaviour
    {
        [SerializeField] private GameObject _plinkoBallPrefab;
        [SerializeField] private GameObject _placedBalls;
        [SerializeField] private GameObject _basket;
        [SerializeField] private GameObject _basketsParent;
        
        [SerializeField] private Color _red;
        [SerializeField] private Color _orange;
        [SerializeField] private Color _yellow;

        private BallSpawner _ballSpawner;
        private DiContainer _container;
        private ScreenSizeManager _screenSizeManager;

        private float _increment = 0f;
        private int _rows = 8;
        private int _risk = 1;
        private float _gap;
        private float _startBaskets;
        private float _startPosY;


        [Inject]
        public void Construct(BallSpawner ballSpawner, DiContainer diContainer, ScreenSizeManager screenSizeManager)
        {
            _screenSizeManager = screenSizeManager;
            _container = diContainer;
            _ballSpawner = ballSpawner;
        }

        public void GeneratePyramind(int rows)
        {
            _rows = rows;

            foreach (Transform child in _placedBalls.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in _basketsParent.transform)
            {
                Destroy(child.gameObject);
            }


            _startPosY = -0.4f * (_screenSizeManager.GetScreenHeight() / 2);
            float startPosX = -_screenSizeManager.GetScreenWidth() / 2 +
                              _plinkoBallPrefab.transform.lossyScale.x / 2;

            _increment = 0.03f + 0.005f * (10 - rows);
            //  float guardSize = 0.375f;

            float[] multipliers = new float[rows + 2];
            // guardSize = 0.375f;
            multipliers = new float[] {13f, 3f, 1.3f, 0.7f, 0.4f, 0.7f, 1.3f, 3f, 13f};


            _gap = (_screenSizeManager.GetScreenWidth() - _plinkoBallPrefab.transform.lossyScale.x) / (rows + 1);

            _startBaskets = startPosX + _gap / 2;

            for (int i = 0; i < rows; i++)
            {
                for (int x = 0; x < rows + 2 - i; x++)
                {
                    GameObject plinkoBall = Instantiate(_plinkoBallPrefab, _placedBalls.transform);
                    plinkoBall.transform.localScale = new Vector2(_screenSizeManager.GetScreenWidth() * _increment,
                        _screenSizeManager.GetScreenWidth() * _increment);
                    plinkoBall.transform.position =
                        new Vector2(startPosX + (_gap * x) + (i * _gap / 2), _startPosY + _gap * i);
                    plinkoBall.name = "row" + i;

                    /*plinkoBall.transform.GetChild(0).transform.localScale = new Vector2(0.1f, 1f);
                    plinkoBall.transform.GetChild(0).transform.localPosition = new Vector2(guardSize, -guardSize);

                    plinkoBall.transform.GetChild(1).transform.localScale = new Vector2(0.1f, 1f);
                    plinkoBall.transform.GetChild(1).transform.localPosition = new Vector2(-guardSize, -guardSize);*/

                    if (i == 0 && x < rows + 1)
                    {
                        var basketSpawned =
                            _container.InstantiatePrefab(_basket.GetComponent<CollectionBasket>(),
                                _basketsParent.transform);

                        //  CollectionBasket basketSpawned = Instantiate(basket, baskets.transform).GetComponent<CollectionBasket>();
                        // _container.Inject(basketSpawned);
                        // Debug.Log("new basket");
                        basketSpawned.name = "basket " + x;

                        if (x == 0 || x == rows)
                        {
                            basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x], _red);
                        }
                        else
                        {
                            if (x == 1 || x == 2 || x == rows - 1 || x == rows - 2)
                            {
                                basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x], _orange);
                            }
                            else
                            {
                                basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x], _yellow);
                            }
                        }


                        basketSpawned.transform.position = new Vector2(_startBaskets + x * _gap, _startPosY - _gap);
                        basketSpawned.transform.localScale = new Vector2(_gap * 0.9f, _gap * 0.9f);
                    }

                    if (i == rows - 1 && x == 1)
                    {
                        //  GameObject spawner = Instantiate(ballSpawner, placedBalls.transform);
                        _ballSpawner.AssignIncrement(_increment);
                        _ballSpawner.SetSpawnPosition(new Vector2(startPosX + (_gap * x) + (i * _gap / 2),
                                _startPosY + _gap * (i + 2)))
                            ;
                    }
                }
            }
        }
    }
}