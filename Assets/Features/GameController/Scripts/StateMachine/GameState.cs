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


        public AbstractGameState(GameState id)
        {
            this.id = id;
            stage = Stage.ENTER;
        }


        protected virtual void Enter()
        {
            stage = Stage.UPDATE;
            // TODO trigger Event from lookup via id
        }

        protected virtual void Update()
        {
            stage = Stage.UPDATE;
        }

        protected virtual void Exit()
        {
            stage = Stage.EXIT;
            // TODO trigger Event from lookup via id
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

        // TODO this shall only be accessed by the game controller
        internal void SetNext(AbstractGameState nextState)
        {
            this.nextState = nextState;
            this.stage = Stage.EXIT;
        }
    }

    public class StartScreenState : AbstractGameState
    {
        public StartScreenState() : base(GameState.START_SCREEN)
        {
        }
    }

    public class LevelInitState : AbstractGameState
    {
        public LevelInitState() : base(GameState.LEVEL_INIT)
        {
        }
    }

    public class GameplayState : AbstractGameState
    {
        public GameplayState() : base(GameState.GAMEPLAY)
        {
        }
    }

    public class PauseScreenState : AbstractGameState
    {
        public PauseScreenState() : base(GameState.PAUSE_SCREEN)
        {
        }
    }

    public class LevelEndState : AbstractGameState
    {
        public LevelEndState() : base(GameState.LEVEL_END)
        {
        }
    }

    public class LevelResultScreenState : AbstractGameState
    {
        public LevelResultScreenState() : base(GameState.LEVEL_RESULT_SCREEN)
        {
        }
    }

    public class GameExitingState : AbstractGameState
    {
        public GameExitingState() : base(GameState.GAME_EXITING)
        {
        }
    }
}
