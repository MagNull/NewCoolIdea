using System;
using UnityEngine;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class AttackState : State
    {
        public AttackState(Func<dynamic> getTarget, Transformable characterTransformable, 
            Func<Weapon> getWeapon, StateMachine stateMachine) 
            : base(getTarget, characterTransformable, getWeapon, stateMachine)
        {
        }

        public override void Enter()
        {
            _attackDistance = _getWeapon.Invoke().MaxAttackDistance;
        }

        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            dynamic target = _getTarget.Invoke();
            if (target is Damageable {IsAlive: true} targetDamageable)
            {
                if (Vector3.SqrMagnitude(targetDamageable.Position - _characterTransformable.Position) >
                    _attackDistance * _attackDistance)
                    _stateMachine.ChangeState<MoveState>();
            }
            else
            {
                _stateMachine.ChangeState<IdleState>();
            }
        }

        public override void Update(float deltaTime)
        {
            _characterTransformable.LookAt(_getTarget.Invoke());
        }
    }
}