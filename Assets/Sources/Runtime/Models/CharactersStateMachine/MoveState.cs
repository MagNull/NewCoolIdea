using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class MoveState : State
    {
        public MoveState(NavMeshAgent navMeshAgent, Func<Damageable> getTarget, 
            Transformable characterTransformable, float attackDistance, StateMachine stateMachine)
            : base(navMeshAgent, getTarget, characterTransformable, attackDistance, stateMachine)
        {
        }
        
        public override void Enter()
        {
            _navMeshAgent.isStopped = false;
        }

        public override void Exit()
        {
            _navMeshAgent.isStopped = true;
        }

        public override void LogicUpdate()
        {
            Damageable targetCharacter = _getTarget.Invoke();
            if (!(targetCharacter is null) && targetCharacter.IsAlive)
            {
                if(Vector3.SqrMagnitude(targetCharacter.Position - _characterTransformable.Position) <= 
                   _attackDistance * _attackDistance)
                    _stateMachine.ChangeState<AttackState>();
            }
            else if(_navMeshAgent.remainingDistance <= 0.3f)
                _stateMachine.ChangeState<IdleState>();
        }

        public override void Update(float deltaTime)
        {
            var targetCharacter = _getTarget.Invoke();
            if (!(targetCharacter is null))
            {
                _navMeshAgent.SetDestination(targetCharacter.Position);
                _characterTransformable.LookAt(targetCharacter);
            }
        }
    }
}