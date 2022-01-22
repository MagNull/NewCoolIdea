using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.CharactersStateMachine;

namespace Sources.Runtime.Models
{
    public class AbilityCaster
    {
        private Ability[] _abilities;
        private StateMachine _stateMachine;
        private State _previousState;
        
        public AbilityCaster(StateMachine stateMachine, Ability[] abilities)
        {
            _stateMachine = stateMachine;
            _abilities = abilities;
        }

        public void OnAbilityCasted(int abilityNumber)
        {
            _previousState = _stateMachine.CurrentState;
            _stateMachine.SwitchActiveAbility(_abilities[abilityNumber - 1]);
            _stateMachine.ChangeState<AbilityCastState>();
        }

        public void OnAbilityCasted()
        {
            _previousState = _stateMachine.CurrentState;
            _stateMachine.SwitchActiveAbility(_abilities[0]);
            _stateMachine.ChangeState<AbilityCastState>();
        }
    }
}