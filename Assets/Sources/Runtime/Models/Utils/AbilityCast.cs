using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.CharactersStateMachine;

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
            if(ability.CanUse)
                _stateMachine.ChangeState(_abilities[abilityNumber - 1]);
        }

        public void Update(float deltaTime)
        {
            foreach (var ability in _abilities)
            {
                ability.CooldownTick(deltaTime);
            }
        }
    }
}