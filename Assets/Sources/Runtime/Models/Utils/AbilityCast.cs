using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class AbilityCast : IUpdatable
    {
        private Ability[] _abilities;
        private StateMachine _stateMachine;
        private bool _canUseAbility = true;

        public AbilityCast(StateMachine stateMachine, Ability[] abilities)
        {
            _stateMachine = stateMachine;
            _abilities = abilities;
        }

        public void OnAbilityUseTried(int abilityNumber)
        {
            var ability = _abilities[abilityNumber - 1];
            if (ability.CanCast &&
                !(_stateMachine.CurrentState is AbilityCastState))
            {
                _stateMachine.SwitchActiveAbility(_abilities[abilityNumber - 1]);
                _stateMachine.ChangeState<AbilityCastState>();
            }
        }

        public void OnAbilityUseTried()
        {
            if (_abilities[0].CanCast)
            {
                _stateMachine.SwitchActiveAbility(_abilities[0]);
                _stateMachine.ChangeState<AbilityCastState>();
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var ability in _abilities)
            {
                ability.Update(deltaTime);
            }
        }
    }
}