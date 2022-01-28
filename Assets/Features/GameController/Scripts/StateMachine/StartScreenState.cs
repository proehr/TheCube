using DataStructures.Event;

namespace Features.GameController.Scripts.StateMachine
{
    public class StartScreenState : AbstractGameState
    {
        public StartScreenState(ActionEvent onBeforeStartScreen, ActionEvent onAfterStartScreen)
            : base(GameState.START_SCREEN, onBeforeStartScreen, onAfterStartScreen) { }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.LEVEL_INIT;
        }
    }
}
