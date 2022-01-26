using Features.GameController.Scripts;
using Features.GameController.Scripts.StateMachine;
using UnityEngine;

namespace Features.GameController
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameState_SO gameStateData;

        private void Awake()
        {
            gameStateData.Set(new StartScreenState());
        }
    }
}
