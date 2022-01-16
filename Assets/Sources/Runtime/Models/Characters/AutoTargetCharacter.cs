using System.Collections.Generic;
using Sources.Runtime.Models.FindTargetStrategies;
using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public abstract class AutoTargetCharacter : Character
    {
        private IFindTargetStrategy _findTargetStrategy;

        public AutoTargetCharacter(Vector3 position, Quaternion rotation, Health health, CharacterBank characterBank,
            float minAttackDistance, float maxAttackDistance) 
            : base(position, rotation, health, characterBank, minAttackDistance, maxAttackDistance)
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