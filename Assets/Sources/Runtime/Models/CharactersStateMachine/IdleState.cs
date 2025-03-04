﻿using System;
using UnityEngine;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class IdleState : State
    {
        private float _attackDistance;
        
        public IdleState(Func<dynamic> getTarget, Transformable characterTransformable, float attackDistance, 
            StateMachine stateMachine) 
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
            if (target is Vector3 targetPoint)
            {
                if(Vector3.SqrMagnitude(_characterTransformable.Position - targetPoint) > 0.09f)
                    _stateMachine.ChangeState<MoveState>();
            }
            else if(target is Damageable {IsAlive: true} targetDamageable)
            {
                if(Vector3.SqrMagnitude(_characterTransformable.Position - targetDamageable.Position) <= 
                   _attackDistance * _attackDistance)
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