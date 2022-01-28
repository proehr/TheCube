using DataStructures.Event;
using Features.Gui.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    public class PauseScreenState : AbstractGameState
    {
        private readonly GuiManager guiManager;

        public PauseScreenState(ActionEvent onBeforePauseScreen, ActionEvent onAfterPauseScreen, GuiManager guiManager)
            : base(GameState.PAUSE_SCREEN, onBeforePauseScreen, onAfterPauseScreen)
        {
            this.guiManager = guiManager;
        }

        protected override void Enter()
        {
            base.Enter();

            guiManager.ShowPauseScreen();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            switch (nextState.id)
            {
                case GameState.GAMEPLAY:
                case GameState.GAME_EXITING:
                    return true;
                default:
                    return false;
            }
        }
    }
}
