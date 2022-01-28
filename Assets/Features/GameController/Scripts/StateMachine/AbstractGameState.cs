using System;
using DataStructures.Event;

namespace Features.GameController.Scripts.StateMachine
{
    // TODO don't let outside namespaces extend from this class
    public abstract class AbstractGameState
    {
        public enum GameState
        {
            START_SCREEN,
            LEVEL_INIT,
            GAMEPLAY,
            PAUSE_SCREEN,
            LEVEL_END,
            LEVEL_RESULT_SCREEN,
            GAME_EXITING
        };

        protected enum Stage
        {
            ENTER,
            UPDATE,
            EXIT
        };

        public GameState id { get; }
        protected Stage stage;

        protected AbstractGameState nextState;
        private readonly ActionEvent onEnterState;
        private readonly ActionEvent onExitState;

        protected AbstractGameState(GameState id, ActionEvent onEnterState, ActionEvent onExitState)
        {
            this.id = id;
            this.stage = Stage.ENTER;
            this.onEnterState = onEnterState;
            this.onExitState = onExitState;
        }

        protected virtual void Enter()
        {
            onEnterState.Raise();
            stage = Stage.UPDATE;
        }

        protected virtual void Update()
        {
            if (this.nextState == null)
            {
                this.stage = Stage.UPDATE;
            }
            else
            {
                this.stage = Stage.EXIT;
            }

        }

        protected virtual void Exit()
        {
            stage = Stage.EXIT;
            onExitState.Raise();
        }

        public AbstractGameState Process()
        {
            if (stage == Stage.ENTER) Enter();
            if (stage == Stage.UPDATE) Update();
            if (stage == Stage.EXIT)
            {
                Exit();
                return nextState;
            }

            return this;
        }

        internal void SetNext(AbstractGameState nextState)
        {
            if (this.nextState == null)
            {
                if (!ValidateNextState(nextState))
                {
                    throw new InvalidOperationException("Invalid Game State transition");
                }
                this.nextState = nextState;
            }
            else
            {
                // Queue state
                this.nextState.SetNext(nextState);
            }
        }

        protected abstract bool ValidateNextState(AbstractGameState nextState);

        public static implicit operator GameState(AbstractGameState state) => state.id;
    }

}
