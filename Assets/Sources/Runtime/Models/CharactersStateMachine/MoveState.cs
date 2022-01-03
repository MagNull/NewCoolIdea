using System;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class MoveState : State
    {
        public MoveState(NavMeshAgent navMeshAgent, Func<Character> getTarget, Character character, StateMachine stateMachine) : base(navMeshAgent, getTarget, character, stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log( _navMeshAgent.gameObject.name+ " enter Move");
            _navMeshAgent.isStopped = false;
        }

        public override void Exit()
        {
            Debug.Log( _navMeshAgent.gameObject.name+ " exit Move");
            _navMeshAgent.isStopped = true;
        }

        public override void LogicUpdate()
        {
            var targetCharacter = _getTarget.Invoke();
            if (targetCharacter is not null)
            {
                if(Vector3.SqrMagnitude(targetCharacter.Position - _character.Position) <= 
                   _character.AttackDistance * _character.AttackDistance)
                    _stateMachine.ChangeState<AttackState>();
            }
            else if(_navMeshAgent.remainingDistance <= 0.1f)
                _stateMachine.ChangeState<IdleState>();
        }
    }
}