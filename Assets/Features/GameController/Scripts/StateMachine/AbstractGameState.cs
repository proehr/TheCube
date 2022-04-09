// #define GAMESTATE_PRINT_DEBUG

using System;
using DataStructures.Event;
#if GAMESTATE_PRINT_DEBUG
using UnityEngine;
#endif

namespace Features.GameController.Scripts.StateMachine
{

    public enum GameState
    {
        /**
             * Default game state when the game is not yet started.
             */
        NONE,

        START_SCREEN,
        LEVEL_INIT,
        GAMEPLAY,
        PAUSE_SCREEN,
        LEVEL_END,
        LAUNCHING,
        LEVEL_RESULT_SCREEN,
        GAME_EXITING
    };

    internal abstract class AbstractGameState
    {
        protected enum Stage
        {
            ENTER,
            UPDATE,
            EXIT
        };

        public GameState id { get; }
        public GameState NextId => nextState?.id ?? GameState.NONE;
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

#if GAMESTATE_PRINT_DEBUG
            Debug.Log(this.GetType().Name + " constructed");
#endif
        }

        protected virtual void Enter()
        {
#if GAMESTATE_PRINT_DEBUG
            Debug.Log(this.GetType().Name + " Enter");
#endif

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
#if GAMESTATE_PRINT_DEBUG
            Debug.Log(this.GetType().Name + " Exit");
#endif

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
#if GAMESTATE_PRINT_DEBUG
            Debug.Log(this.GetType().Name + " SetNext " + nextState.GetType().Name);
#endif

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
