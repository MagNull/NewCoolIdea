using System;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class MoveState : State
    {
        public MoveState(NavMeshAgent navMeshAgent, Func<Character> getTarget, Transformable characterTransformable, float attackDistance, StateMachine stateMachine) : base(navMeshAgent, getTarget, characterTransformable, attackDistance, stateMachine)
        {
        }
        
        public override void Enter()
        {
            //Debug.Log( _navMeshAgent.gameObject.name+ " enter Move");
            _navMeshAgent.isStopped = false;
        }

        public override void Exit()
        {
           // Debug.Log( _navMeshAgent.gameObject.name+ " exit Move");
            _navMeshAgent.isStopped = true;
        }

        public override void LogicUpdate()
        {
            Character targetCharacter = _getTarget.Invoke();
            if (!(targetCharacter is null))
            {
                if(Vector3.SqrMagnitude(targetCharacter.Position - _characterTransformable.Position) <= 
                   _attackDistance * _attackDistance)
                    _stateMachine.ChangeState<AttackState>();
            }
            else if(_navMeshAgent.remainingDistance <= 0.1f)
                _stateMachine.ChangeState<IdleState>();
        }

        public override void Update(float deltaTime)
        {
            var targetCharacter = _getTarget.Invoke();
            if (!(targetCharacter is null))
            {
                _navMeshAgent.SetDestination(targetCharacter.Position);
            }
        }
    }
}