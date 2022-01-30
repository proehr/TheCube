using DataStructures.Event;
using Features.GameController.Scripts.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GameController.Scripts
{
    [CreateAssetMenu(fileName = "GameState", menuName = "Data/GameState")]
    public class GameState_SO : ScriptableObject
    {
        private AbstractGameState gameState;
        [SerializeField] private GameEvent onGameStateChanged;

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
                if (onGameStateChanged != null) onGameStateChanged.Raise();
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
            var previousStateId = gameState.id;
            gameState = gameState.Process();
            if (previousStateId != gameState.id && onGameStateChanged != null)
            {
                onGameStateChanged.Raise();
            }
        }
    }
}
