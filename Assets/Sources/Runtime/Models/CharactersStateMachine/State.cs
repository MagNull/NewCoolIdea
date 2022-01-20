﻿using System;
namespace Sources.Runtime.Models.CharactersStateMachine
{
    public abstract class State : IUpdatable
    {
        protected readonly Func<dynamic> _getTarget;
        protected float _attackDistance;
        protected readonly StateMachine _stateMachine;
        protected readonly Transformable _characterTransformable;

        protected State(Func<dynamic> getTarget, Transformable characterTransformable,
            float attackDistance, StateMachine stateMachine)
        {
            _getTarget = getTarget;
            _attackDistance = attackDistance;
            _characterTransformable = characterTransformable;
            _stateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void LogicUpdate();
        public abstract void Update(float deltaTime);
    }
}