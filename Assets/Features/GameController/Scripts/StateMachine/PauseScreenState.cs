using DataStructures.Event;
using Features.Gui.Scripts;
using UnityEngine;

namespace Features.GameController.Scripts.StateMachine
{
    internal class PauseScreenState : AbstractGameState
    {
        public PauseScreenState(ActionEvent onBeforePauseScreen, ActionEvent onAfterPauseScreen)
            : base(GameState.PAUSE_SCREEN, onBeforePauseScreen, onAfterPauseScreen)
        {
        }

        protected override void Enter()
        {
            base.Enter();
            
            Time.timeScale = 0f;
        }

        protected override void Exit()
        {
            base.Exit();
            
            Time.timeScale = 1f;
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            switch (nextState.id)
            {
                case GameState.LEVEL_END:
                case GameState.GAMEPLAY:
                case GameState.GAME_EXITING:
                    return true;
                default:
                    return false;
            }
        }
    }
}
