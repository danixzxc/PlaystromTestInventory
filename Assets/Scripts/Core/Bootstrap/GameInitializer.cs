using UnityEngine;

namespace Core.Bootstrap
{
    public class GameInitializer : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("Game Initialized - Waiting for DI bindings");
        }
    }
}