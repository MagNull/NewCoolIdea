using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class MoveState : State
    {
        private readonly NavMeshAgent _navMeshAgent;

        public MoveState(NavMeshAgent navMeshAgent, Func<dynamic> getTarget, 
            Transformable characterTransformable, float attackDistance, StateMachine stateMachine)
            : base(getTarget, characterTransformable, attackDistance, stateMachine)
        {
            _navMeshAgent = navMeshAgent;
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
            dynamic target = _getTarget.Invoke();
            if (target is Damageable {IsAlive: true} targetDamageable)
            {
                if (Vector3.SqrMagnitude(targetDamageable.Position - _characterTransformable.Position) <=
                    _attackDistance * _attackDistance)
                {
                    _stateMachine.ChangeState<AttackState>();
                }
            }
            else if (target is Vector3 targetPoint &&
                     Vector3.SqrMagnitude(_characterTransformable.Position - targetPoint) <= 0.09f)
            { 
                _stateMachine.ChangeState<IdleState>();
            }
        }

        public override void Update(float deltaTime)
        {
            dynamic target = _getTarget.Invoke();
            if (target is Transformable targetTransformable)
            {
                _navMeshAgent.SetDestination(targetTransformable.Position);
                _characterTransformable.LookAt(targetTransformable);
            }
            else if(target is Vector3 targetPoint)
            {
                _navMeshAgent.SetDestination(targetPoint);
                _characterTransformable.LookAt(targetPoint);
            }
            _characterTransformable.MoveTo(_navMeshAgent.nextPosition);
        }
    }
}