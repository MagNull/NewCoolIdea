﻿using System.Collections.Generic;
using Sources.Runtime.Models.FindTargetStrategies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    public abstract class AutoTargetCharacter : Character
    {
        private IFindTargetStrategy _findTargetStrategy;

        public AutoTargetCharacter(Vector3 position, Quaternion rotation, Health health, float attackDistance) 
            : base(position, rotation, health, attackDistance)
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