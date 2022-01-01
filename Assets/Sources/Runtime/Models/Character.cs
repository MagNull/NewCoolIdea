using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models
{
    public class Character : Transformable, IUpdatable
    {
        public NavMeshAgent NavMeshAgent;

        private Character _targetCharacter;

        public Character(Vector3 position, Quaternion rotation, NavMeshAgent navMeshAgent) : base(position, rotation)
        {
            NavMeshAgent = navMeshAgent;
        }
        
        protected void SetTarget(object target)
        {
            _targetCharacter = null;
            if (target is Vector3 targetPos)
                NavMeshAgent.SetDestination(targetPos);
            if (target is Character targetCharacter)
                _targetCharacter = targetCharacter;
        }
        
        public void Update()
        {
            if (_targetCharacter is not null)
            {
                NavMeshAgent.SetDestination(_targetCharacter.Position);
                MoveTo(NavMeshAgent.nextPosition);
            }
        }
    }
}