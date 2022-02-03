using System;
using Sources.Runtime.Models.CharactersStateMachine;

namespace Sources.Runtime.Models.Abilities
{
    public abstract class Ability : State
    {
        private float _coolDown;
        private float _coolDownTimer;
        public bool CanUse => _coolDownTimer <= 0;
        
        protected Ability(Func<dynamic> getTarget, Transformable characterTransformable, StateMachine stateMachine, 
            float coolDown) 
            : base(getTarget, characterTransformable, stateMachine)
        {
            _coolDown = coolDown;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            _coolDownTimer = _coolDown;
        }

        public override void LogicUpdate()
        {
            
        }

        public override void Update(float deltaTime)
        {
            
        }

        public void CooldownTick(float deltaTime)
        {
            if(_coolDownTimer > 0)
                _coolDownTimer -= deltaTime;
        }
    }
}