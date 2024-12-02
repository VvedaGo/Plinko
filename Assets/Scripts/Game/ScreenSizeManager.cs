using UnityEngine;

namespace Assets.Scripts.Game
{
    public class ScreenSizeManager
    {
        public float GetScreenHeight()
        {
            return Camera.main.orthographicSize * 2f;
        }

        public float GetScreenWidth()
        {
            return GetScreenHeight() * Screen.width / Screen.height;
        }
    }
}