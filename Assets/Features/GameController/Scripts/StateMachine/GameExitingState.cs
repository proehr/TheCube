using DataStructures.Event;
using Features.SaveLoad.Scripts;
#if UNITY_EDITOR
using UnityEditor;
#else
using UnityEngine;
#endif

namespace Features.GameController.Scripts.StateMachine
{
    internal class GameExitingState : AbstractGameState
    {
        private readonly SaveGameManager saveGameManager;

        public GameExitingState(ActionEvent onBeforeGameExiting, ActionEvent onAfterGameExiting, SaveGameManager saveGameManager)
            : base(GameState.GAME_EXITING, onBeforeGameExiting, onAfterGameExiting)
        {
            this.saveGameManager = saveGameManager;
        }

        protected override void Enter()
        {
            base.Enter();

            saveGameManager.Save();
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            // This is the final state - no further transition is legal
            return false;
        }
    }
}
