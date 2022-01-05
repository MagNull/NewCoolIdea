using System;
using System.Timers;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class AttackState : State
    {
        private Timer _testAttackTimer;
        public AttackState(NavMeshAgent navMeshAgent, Func<Character> getTarget, Character character, StateMachine stateMachine) : base(navMeshAgent, getTarget, character, stateMachine)
        {
            _testAttackTimer = new Timer(1500);
            _testAttackTimer.Elapsed += (sender, args) => _getTarget.Invoke().TakeDamage(1);
            _testAttackTimer.AutoReset = true;
        }

        public override void Enter()
        {
            _testAttackTimer.Enabled = true;
           // Debug.Log( _navMeshAgent.gameObject.name+ " enter Attack");
        }

        public override void Exit()
        {
            _testAttackTimer.Enabled = false;
           // Debug.Log( _navMeshAgent.gameObject.name+ " exit Attack");
        }

        public override void LogicUpdate()
        {
            Character targetCharacter = _getTarget.Invoke();
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

        public override void Update(float deltaTime)
        {
            
        }
    }
}