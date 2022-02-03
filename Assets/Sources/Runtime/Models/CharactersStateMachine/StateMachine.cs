using System;
using System.Linq;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class StateMachine : IUpdatable
    {
        public event Action<State> StateChanged;
        private State[] _states;
        private State _currentState;

        public State CurrentState => _currentState;

        public void Init(State[] states, State startState)
        {
            _states = states;
            _currentState = startState;
        }
        
        public void ChangeState<T>() where T : State
        {
            CurrentState.Exit();
            
            _currentState = _states.FirstOrDefault(state => state is T);
            CurrentState.Enter();
            StateChanged?.Invoke(CurrentState);
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();

            _currentState = newState;
            CurrentState.Enter();
            StateChanged?.Invoke(CurrentState);
        }

        public void Update(float deltaTime)
        {
            CurrentState.LogicUpdate();
            CurrentState.Update(deltaTime);
        }
    }
}