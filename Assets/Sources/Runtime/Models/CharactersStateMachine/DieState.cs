using System;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class DieState : State
    {
        public DieState(Func<dynamic> getTarget, Transformable
            characterTransformable, StateMachine stateMachine) 
            : base(getTarget, characterTransformable, stateMachine)
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