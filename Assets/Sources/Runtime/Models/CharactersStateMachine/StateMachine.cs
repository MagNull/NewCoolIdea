using System.Linq;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class StateMachine : IUpdatable
    {
        private State[] _states;
        private State _currentState;

        public void Init(State[] states, State startState)
        {
            _states = states;
            
            _currentState = startState;
            _currentState.Enter();
        }
        
        public void ChangeState<T>() where T : State
        {
            _currentState.Exit();

            _currentState = _states.FirstOrDefault(state => state is T);
            _currentState.Enter();
        }

        public void Update(float deltaTime)
        {
            _currentState.LogicUpdate();
        }
    }
}