using System;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class AttackState : State
    {
        public AttackState(NavMeshAgent navMeshAgent, Func<Character> getTarget, Character character, StateMachine stateMachine) : base(navMeshAgent, getTarget, character, stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log( _navMeshAgent.gameObject.name+ " enter Attack");
        }

        public override void Exit()
        {
            Debug.Log( _navMeshAgent.gameObject.name+ " exit Attack");
        }

        public override void LogicUpdate()
        {
            var targetCharacter = _getTarget.Invoke();
            if (targetCharacter is not null)
            {
                if (Vector3.SqrMagnitude(targetCharacter.Position - _character.Position) >
                    _character.AttackDistance * _character.AttackDistance)
                    _stateMachine.ChangeState<MoveState>();
            }
            else
            {
                if(_navMeshAgent.remainingDistance <= 0.1f)
                    _stateMachine.ChangeState<IdleState>();
                else
                    _stateMachine.ChangeState<MoveState>();
            }
        }
    }
}