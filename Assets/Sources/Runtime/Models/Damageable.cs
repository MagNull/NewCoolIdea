using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    [Serializable]
    public abstract class Damageable : Transformable
    {
        public virtual Health Health { get; private set; }
        public virtual bool IsAlive { get; private set; }

        protected Damageable(Vector3 position, Quaternion rotation, int healthValue) : base(position, rotation)
        {
            IsAlive = true;
            Health = new Health(healthValue);
            Health.Died += Die;
        }

        public virtual void Die()
        {
            IsAlive = false;
        }
    }
}