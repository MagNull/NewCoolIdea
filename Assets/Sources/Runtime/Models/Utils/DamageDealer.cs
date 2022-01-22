using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class DamageDealer : Transformable
    {
        private readonly int _damage;
        
        public void OnCollision(Character character)
        {
            character.Health.TakeDamage(_damage);
            Destroy();
        }

        protected DamageDealer(Vector3 position, Quaternion rotation, int damage) : base(position, rotation)
        {
            _damage = damage;
        }
    }
}