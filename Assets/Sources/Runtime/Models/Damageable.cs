using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    [Serializable]
    public abstract class Damageable : Transformable
    {
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public bool IsAlive { get; private set; }

        protected Damageable(Vector3 position, Quaternion rotation, int healthValue) : base(position, rotation)
        {
            IsAlive = true;
            Health = new Health(healthValue);
            Health.Died += () => IsAlive = false;
        }
    }
}