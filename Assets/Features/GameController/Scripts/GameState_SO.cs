using Features.GameController.Scripts.StateMachine;
using UnityEngine;
namespace Features.GameController.Scripts
{
    [CreateAssetMenu(fileName = "GameState", menuName = "Data/GameState")]
    public class GameState_SO : ScriptableObject
    {
        [SerializeField] private AbstractGameState gameState;

        // TODO this shall only be accessed by the game controller
        internal void Set(AbstractGameState gameState)
        {
            if (this.gameState == null)
            {
                this.gameState = gameState;
            }
            else
            {
                // TODO FIXME if the gameState already has a nextState scheduled, go through all their stages and set next on the previously scheduled state
                this.gameState.SetNext(gameState);
            }
        }

        public AbstractGameState Get()
        {
            return gameState;
        }

        public AbstractGameState.GameState GetId()
        {
            return gameState.id;
        }

        internal void Update()
        {
            gameState = gameState.Process();
        }
    }
}
