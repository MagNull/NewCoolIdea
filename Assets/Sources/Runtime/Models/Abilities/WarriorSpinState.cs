using System;
using Sources.Runtime.Models.CharactersStateMachine;

namespace Sources.Runtime.Models.Abilities
{
    public class WarriorSpinState : Ability
    {
        private float _duration;
        private float _durationTimer;
        private MeleeWeapon _weapon;

        public WarriorSpinState(Func<dynamic> getTarget, Transformable characterTransformable, StateMachine stateMachine,
            float duration, Weapon weapon) 
            : base(getTarget, characterTransformable, stateMachine)
        {
            _duration = duration;
            _weapon = weapon as MeleeWeapon;
        }

        public override void Enter()
        {
            base.Enter();
            _durationTimer = _duration;
            _weapon.Activate();
        }

        public override void Exit()
        {
            _weapon.Deactivate();
        }

        public override void LogicUpdate()
        {
            if(_durationTimer <= 0)
                _stateMachine.ChangeState<IdleState>();
        }

        public override void Update(float deltaTime)
        {
            _durationTimer -= deltaTime;
        }
    }
}