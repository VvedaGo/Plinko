using UnityEngine;

namespace Assets.Scripts.Game
{
    public class StaticBall : MonoBehaviour
    {
       
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _redTimerLength = 0.5f;

        private float _timer;
        private bool _isColored;
        private bool _timerOn;


        public void StartBop()
        {
            if (!_isColored)
            {
                _spriteRenderer.color = Color.red;
                _timer = 0f;
                _timerOn = true;
                _isColored = true;
            }
        }

        void Update()
        {
            if (_timerOn)
            {
                _timer += Time.deltaTime;
                if (_timer > _redTimerLength)
                {
                    _spriteRenderer.color = Color.white;
                    _timerOn = false;
                    _isColored = false;
                }
            }
        }
    }
}