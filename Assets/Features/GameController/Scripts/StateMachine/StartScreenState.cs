using DataStructures.Event;
using Features.Gui.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class StartScreenState : AbstractGameState
    {
        private readonly GuiController guiController;

        public StartScreenState(ActionEvent onBeforeStartScreen, ActionEvent onAfterStartScreen, GuiController guiController)
            : base(GameState.START_SCREEN, onBeforeStartScreen, onAfterStartScreen)
        {
            this.guiController = guiController;
        }

        protected override void Enter()
        {
            base.Enter();

            guiController.ShowStartScreen();
        }

        protected override void Exit()
        {
            guiController.HideStartScreen();

            base.Exit();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.LEVEL_INIT;
        }
    }
}
