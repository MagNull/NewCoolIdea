using UnityEngine;

namespace Sources.Runtime.Models
{
    public class DamageDealer : Transformable, IActivable
    {
        private readonly int _damage;
        private bool _isActivate = false;
        
        public virtual void OnCollision(Damageable character)
        {
            if(_isActivate) 
                character.Health.TakeDamage(_damage);
        }

        public DamageDealer(Vector3 position, Quaternion rotation, int damage) : base(position, rotation)
        {
            _damage = damage;
        }

        public void Activate() => _isActivate = true;

        public void Deactivate() => _isActivate = false;
    }
}