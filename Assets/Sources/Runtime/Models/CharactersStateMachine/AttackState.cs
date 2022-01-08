using System;
using System.Timers;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class AttackState : State
    {
        public AttackState(NavMeshAgent navMeshAgent, Func<Character> getTarget, Transformable characterTransformable, float attackDistance, StateMachine stateMachine) : base(navMeshAgent, getTarget, characterTransformable, attackDistance, stateMachine)
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
            Character targetCharacter = _getTarget.Invoke();
            if (!(targetCharacter is null))
            {
                if (Vector3.SqrMagnitude(targetCharacter.Position - _characterTransformable.Position) >
                    _attackDistance * _attackDistance)
                    _stateMachine.ChangeState<MoveState>();
            }
            else
            {
                if(_navMeshAgent.remainingDistance <= 0.3f)
                    _stateMachine.ChangeState<IdleState>();
                else
                    _stateMachine.ChangeState<MoveState>();
            }
        }

        public override void Update(float deltaTime)
        {
            _characterTransformable.LookAt(_getTarget.Invoke());
        }
    }
}