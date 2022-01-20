using System;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class AbilityCastState : State
    {
        public int AbilityNumber { get; set; }
        
        public AbilityCastState(Func<dynamic> getTarget,
            Transformable characterTransformable, float attackDistance, StateMachine stateMachine) 
            : base(getTarget, characterTransformable, attackDistance, stateMachine)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}