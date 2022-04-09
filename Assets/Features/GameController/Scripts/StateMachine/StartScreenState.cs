using DataStructures.Event;
using Features.Gui.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class StartScreenState : AbstractGameState
    {
        public StartScreenState(ActionEvent onBeforeStartScreen, ActionEvent onAfterStartScreen, CanvasManager canvasManager)
            : base(GameState.START_SCREEN, onBeforeStartScreen, onAfterStartScreen)
        {
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            switch (nextState.id)
            {
                case GameState.LEVEL_INIT:
                case GameState.GAME_EXITING:
                    return true;
                default:
                    return false;
            }
        }
    }
}
