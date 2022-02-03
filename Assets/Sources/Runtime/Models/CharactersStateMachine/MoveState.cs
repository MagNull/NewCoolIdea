using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class MoveState : State
    {
        private float _attackDistance;

        public MoveState(Func<dynamic> getTarget, Transformable characterTransformable,
            float attackDistance, StateMachine stateMachine) 
            : base(getTarget, characterTransformable, stateMachine)
        {
            _attackDistance = attackDistance;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
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
            
        }
    }
}