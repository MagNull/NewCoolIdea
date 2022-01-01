using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models
{
    [Serializable]
    public class Character : Transformable, IUpdatable
    {
        public NavMeshAgent NavMeshAgent;

        private Character _targetCharacter;
        private Health _health;

        public Character(Vector3 position, Quaternion rotation, 
            NavMeshAgent navMeshAgent, Health health) : base(position, rotation)
        {
            NavMeshAgent = navMeshAgent;
            _health = health;
        }

        public void TakeDamage(int damage) => _health.TakeDamage(damage);
        
        protected void SetTarget(object target)
        {
            _targetCharacter = null;
            if (target is Vector3 targetPos)
                NavMeshAgent.SetDestination(targetPos);
            if (target is Character targetCharacter)
                _targetCharacter = targetCharacter;
        }
        
        public virtual void Update()
        {
            if (_targetCharacter is not null)
            {
                NavMeshAgent.SetDestination(_targetCharacter.Position);
            }
            MoveTo(NavMeshAgent.nextPosition);
        }
    }
}