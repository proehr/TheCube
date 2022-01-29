using DataStructures.Event;
using Features.Gui.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class PauseScreenState : AbstractGameState
    {
        private readonly GuiController guiController;

        public PauseScreenState(ActionEvent onBeforePauseScreen, ActionEvent onAfterPauseScreen, GuiController guiController)
            : base(GameState.PAUSE_SCREEN, onBeforePauseScreen, onAfterPauseScreen)
        {
            this.guiController = guiController;
        }

        protected override void Enter()
        {
            base.Enter();

            guiController.ShowPauseScreen();
        }

        protected override void Exit()
        {
            guiController.HidePauseScreen();

            base.Exit();
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
