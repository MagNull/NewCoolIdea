using System;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class IdleState : State
    {
        public IdleState(NavMeshAgent navMeshAgent, Func<Damageable> getTarget, 
            Transformable characterTransformable, float attackDistance, StateMachine stateMachine) 
            : base(navMeshAgent, getTarget, characterTransformable, attackDistance, stateMachine)
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
            Damageable targetCharacter = _getTarget.Invoke();
            if (targetCharacter is null)
            {
                if(_navMeshAgent.remainingDistance > 0.3f)
                    _stateMachine.ChangeState<MoveState>();
            }
            else if(targetCharacter.IsAlive)
            {
                if(Vector3.SqrMagnitude(_characterTransformable.Position - targetCharacter.Position) <= 
                   _attackDistance * _attackDistance)
                    _stateMachine.ChangeState<AttackState>();
                else
                    _stateMachine.ChangeState<MoveState>();
            }
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}