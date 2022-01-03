using System.Collections.Generic;
using Sources.Runtime.Models.FindTargetStrategies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    public abstract class AutoTargetCharacter : Character
    {
        private IFindTargetStrategy _findTargetStrategy;
        protected List<Character> _targets;

        public AutoTargetCharacter(Vector3 position, Quaternion rotation, 
            NavMeshAgent navMeshAgent, Health health, CharacterBank bank) : base(position, rotation, navMeshAgent, health)
        {
            _findTargetStrategy = new FindNearestStrategy();
        }

        public override void Update(float deltaTime)
        {
            SetTarget(_findTargetStrategy.GetTarget(_targets, this));
            base.Update(deltaTime);
        }
    }
}