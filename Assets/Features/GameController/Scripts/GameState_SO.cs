using Features.GameController.Scripts.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GameController.Scripts
{
    [CreateAssetMenu(fileName = "GameState", menuName = "Data/GameState")]
    public class GameState_SO : ScriptableObject
    {
        private AbstractGameState gameState;

#if UNITY_EDITOR
        // Display readable game states for developers

        [ShowInInspector, ReadOnly] private GameState GameState
        {
            get
            {
                if (gameState == null)
                {
                    return GameState.NONE;
                }

                return gameState.id;
            }
        }
        [ShowInInspector, ReadOnly] private GameState NextGameState
        {
            get
            {
                if (gameState == null)
                {
                    return GameState.NONE;
                }

                return gameState.NextId;
            }
        }
#endif

        internal void Set(AbstractGameState gameState)
        {
            if (this.gameState == null)
            {
                this.gameState = gameState;
            }
            else
            {
                this.gameState.SetNext(gameState);
            }
        }

        public GameState Get()
        {
            return gameState.id;
        }

        internal void Update()
        {
            gameState = gameState.Process();
        }
    }
}
