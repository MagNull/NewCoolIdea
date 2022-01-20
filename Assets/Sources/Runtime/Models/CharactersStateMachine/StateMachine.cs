using System;
using System.Linq;
using UnityEngine;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class StateMachine : IUpdatable
    {
        public event Action<State> StateChanged;
        private State[] _states;
        private State _currentState;
        private AbilityCastState _abilityCastState;
        
        public void Init(State[] states, State startState)
        {
            _states = states;
            _abilityCastState = (AbilityCastState) _states.FirstOrDefault(x => x is AbilityCastState);
            _currentState = startState;
        }
        
        public void ChangeState<T>() where T : State
        {
            _currentState.Exit();

            _currentState = _states.FirstOrDefault(state => state is T);
            _currentState.Enter();
            StateChanged?.Invoke(_currentState);
        }

        public void ChangeAbilityNumber(int num) => _abilityCastState.AbilityNumber = num;

        public void Update(float deltaTime)
        {
            _currentState.LogicUpdate();
            _currentState.Update(deltaTime);
        }
    }
}