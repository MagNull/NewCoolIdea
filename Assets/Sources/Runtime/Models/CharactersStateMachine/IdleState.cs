using System;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class IdleState : State
    {
        public IdleState(NavMeshAgent navMeshAgent, Func<Character> getTarget, Character character, 
            StateMachine stateMachine) 
            : base(navMeshAgent, getTarget, character, stateMachine)
        {
        }

        public override void Enter()
        {
            //Debug.Log( _navMeshAgent.gameObject.name+ " enter Idle");
        }

        public override void Exit()
        {
            //Debug.Log( _navMeshAgent.gameObject.name + " exit Idle");
        }

        public override void LogicUpdate()
        {
            var targetCharacter = _getTarget.Invoke();
            if (targetCharacter is null)
            {
                if(_navMeshAgent.remainingDistance > 0.1f)
                    _stateMachine.ChangeState<MoveState>();
            }
            else
            {
                if(Vector3.SqrMagnitude(_character.Position - targetCharacter.Position) <= 
                   _character.AttackDistance * _character.AttackDistance)
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