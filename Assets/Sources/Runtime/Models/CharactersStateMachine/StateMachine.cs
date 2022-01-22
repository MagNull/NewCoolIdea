using System;
using System.Linq;
using Sources.Runtime.Models.Abilities;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class StateMachine : IUpdatable
    {
        public event Action<State> StateChanged;
        private State[] _states;
        private State _currentState;
        private AbilityCastState _abilityCastState;

        public State CurrentState => _currentState;

        public void Init(State[] states, State startState)
        {
            _states = states;
            _abilityCastState = (AbilityCastState) _states.FirstOrDefault(x => x is AbilityCastState);
            _currentState = startState;
        }
        
        public void ChangeState<T>() where T : State
        {
            CurrentState.Exit();

            _currentState = _states.FirstOrDefault(state => state is T);
            CurrentState.Enter();
            StateChanged?.Invoke(CurrentState);
        }

        public void SwitchActiveAbility(Ability ability) => _abilityCastState.Ability = ability;

        public void Update(float deltaTime)
        {
            CurrentState.LogicUpdate();
            CurrentState.Update(deltaTime);
        }
    }
}