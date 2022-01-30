using DataStructures.Event;

namespace Features.GameController.Scripts.StateMachine
{
    internal class GameplayState : AbstractGameState
    {
        public GameplayState(ActionEvent onBeforeGameplay, ActionEvent onAfterGameplay)
            : base(GameState.GAMEPLAY, onBeforeGameplay, onAfterGameplay) { }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            switch (nextState.id)
            {
                case GameState.PAUSE_SCREEN:
                case GameState.LEVEL_END:
                    return true;
                default:
                    return false;
            }
        }
    }
}
