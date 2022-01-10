using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    [Serializable]
    public abstract class Damageable : Transformable
    {
        public Health Health { get; }
        public bool IsAlive { get; private set; }

        protected Damageable(Vector3 position, Quaternion rotation, Health health) : base(position, rotation)
        {
            IsAlive = true;
            Health = health;
            Health.Died += () => IsAlive = false;
        }
    }
}