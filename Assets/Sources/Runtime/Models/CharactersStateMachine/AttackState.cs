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
        
        public AttackState(NavMeshAgent navMeshAgent, Func<Character> getTarget, Transformable characterTransformable, float attackDistance, StateMachine stateMachine) : base(navMeshAgent, getTarget, characterTransformable, attackDistance, stateMachine)
        {
            _testAttackTimer = new Timer(1500);
            _testAttackTimer.Elapsed += (sender, args) => _getTarget.Invoke().TakeDamage(1);
            _testAttackTimer.AutoReset = true;
        }

        public override void Enter()
        {
            _testAttackTimer.Enabled = true;
        }

        public override void Exit()
        {
            _testAttackTimer.Enabled = false;
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