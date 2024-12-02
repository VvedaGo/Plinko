using UnityEngine;

namespace Assets.Scripts.Game
{
   public class BallScript : MonoBehaviour
    {
        [SerializeField] private ForceMode2D _forcemode = ForceMode2D.Impulse;

        [SerializeField] private float _thrust = 1f;

        [SerializeField]private Rigidbody2D _rigidbody2D;

        private string _lastHit = "";
        private float _betAmount = 0f;
        private ScreenSizeManager _screenSizeManager;

    

        public void Construct(ScreenSizeManager screenSizeManager)
        {
            _screenSizeManager = screenSizeManager;
        }

        public void Setup(float increment, float betAmount = 5f)
        {
            transform.localScale = new Vector2(_screenSizeManager.GetScreenWidth() * increment,
                _screenSizeManager.GetScreenWidth() * increment);
            _betAmount = betAmount;
        }

        public float GetBetValue()
        {
            return _betAmount;
        }


        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (collision2D.gameObject.TryGetComponent(out StaticBall staticBall) && _lastHit != collision2D.gameObject.name)
            {
                _lastHit = collision2D.gameObject.name;
                int randomValue = UnityEngine.Random.Range(0, 100);

                staticBall.StartBop();

                if (randomValue > 50)
                {
                    _rigidbody2D.AddForce(new Vector2(_thrust, _thrust * 0.02f), _forcemode);
                }
                else
                {
                    _rigidbody2D.AddForce(new Vector2(-_thrust, _thrust * 0.02f), _forcemode);
                }
            }
        }

        public void ResetState()
        {
            gameObject.SetActive(false);
        }
    }
}