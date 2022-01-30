using DataStructures.Event;
using Features.SaveLoad.Scripts;
using UnityEngine;

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
            Application.Quit();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            // This is the final state - no further transition is legal
            return false;
        }
    }
}
