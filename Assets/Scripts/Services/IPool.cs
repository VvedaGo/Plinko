using UnityEngine;

namespace Assets.Scripts.Services
{
    public interface IPool<T> where T : MonoBehaviour
    {
        T GetFreeElement();
    }
}