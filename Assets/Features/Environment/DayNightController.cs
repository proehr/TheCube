using Features.GameController.Scripts;
using Features.GameController.Scripts.StateMachine;
using UnityEngine;

namespace Features.Environment
{
    public class DayNightController : MonoBehaviour
    {
        [SerializeField] private GameState_SO gameState;
        
        [SerializeField] private Transform mainLight;
        [SerializeField] private Transform rimLight;
        
        [SerializeField] private float value = 0.05f;

        private void Update()
        {
            if (gameState.Get() != GameState.GAMEPLAY) return;
            
            mainLight.Rotate(value, 0 ,0);
            rimLight.Rotate(value, 0 ,0);
        }
    }
}
