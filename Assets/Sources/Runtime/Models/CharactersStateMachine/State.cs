using System;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public abstract class State : IUpdatable
    {
        protected readonly NavMeshAgent _navMeshAgent;
        protected readonly Func<Damageable> _getTarget;
        protected float _attackDistance;
        protected readonly StateMachine _stateMachine;
        protected Transformable _characterTransformable;

        public State(NavMeshAgent navMeshAgent, Func<Damageable> getTarget, Transformable characterTransformable,
            float attackDistance, StateMachine stateMachine)
        {
            _navMeshAgent = navMeshAgent;
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