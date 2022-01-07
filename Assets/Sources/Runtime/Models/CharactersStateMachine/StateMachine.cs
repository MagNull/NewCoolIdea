using System;
using System.Linq;
using Sources.Runtime.Models.Characters;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class StateMachine : IUpdatable
    {
        private State[] _states;
        private State _currentState;
        private Character _character;

        public StateMachine(Character character)
        {
            _character = character;
        }
        public void Init(State[] states, State startState)
        {
            _states = states;
            _currentState = startState;
        }
        
        public void ChangeState<T>() where T : State
        {
            _currentState.Exit();

            _currentState = _states.FirstOrDefault(state => state is T);
            _currentState.Enter();
            _character.StateChanged?.Invoke(_currentState);
        }

        public void Update(float deltaTime)
        {
            _currentState.Update(deltaTime);
            _currentState.LogicUpdate();
        }
    }
}