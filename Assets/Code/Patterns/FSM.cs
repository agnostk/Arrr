namespace Patterns
{
    public abstract class FSM
    {
        private State _currentState;
        public State CurrentState
        {
            get => _currentState;
            set => SetState(value);
        }

        public void SetState(State state)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            _currentState = state;

            if (CurrentState != null)
            {
                CurrentState.Enter();
            }
        }
    }
}