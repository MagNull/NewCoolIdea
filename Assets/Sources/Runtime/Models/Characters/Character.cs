using System;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    [Serializable]
    public class Character : Transformable, IUpdatable
    {
        private float _attackDistance = 3;
        private NavMeshAgent _navMeshAgent;
        private Character _targetCharacter;
        private Health _health;
        private StateMachine _stateMachine;


        public Character(Vector3 position, Quaternion rotation, 
            NavMeshAgent navMeshAgent, Health health) : base(position, rotation)
        {
            _navMeshAgent = navMeshAgent;
            _health = health;
            
            _stateMachine = new StateMachine();
            var states = GetStates();
            _stateMachine.Init(states, states[0]);
        }

        private State[] GetStates()
        {
            var states = new State[3];
            states[0] = new IdleState(_navMeshAgent, GetTarget, this, _stateMachine);
            states[1] = new MoveState(_navMeshAgent, GetTarget, this, _stateMachine);
            states[2] = new AttackState(_navMeshAgent, GetTarget, this, _stateMachine);

            return states;
        }

        public float AttackDistance => _attackDistance;

        public void TakeDamage(int damage) => _health.TakeDamage(damage);
        
        protected void SetTarget(object target)
        {
            _targetCharacter = null;
            if (target is Vector3 targetPos)
                _navMeshAgent.SetDestination(targetPos);
            if (target is Character targetCharacter)
                _targetCharacter = targetCharacter;
        }
        
        public virtual void Update(float deltaTime)
        {
            if (_targetCharacter is not null)
            {
                _navMeshAgent.SetDestination(_targetCharacter.Position);
            }
            MoveTo(_navMeshAgent.nextPosition);
            _stateMachine.Update(deltaTime);
        }

        private Character GetTarget() => _targetCharacter;
        
    }
}