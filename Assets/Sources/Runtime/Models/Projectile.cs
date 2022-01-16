using UnityEngine;

namespace Sources.Runtime.Models
{
    public class Projectile : Transformable, IUpdatable
    {
        private int _damage;
        private float _speed;
        private Damageable _target;
        
        public Projectile(Vector3 position, Quaternion rotation, 
            Damageable target, float speed, int damage) 
            : base(position, rotation)
        {
            _target = target;
            _speed = speed;
            _damage = damage;
        }

        public void Update(float deltaTime)
        {
            Vector3 newPosition = 
                Vector3.MoveTowards(Position, _target.Position, _speed * deltaTime);
            MoveTo(newPosition);
        }

        public void OnCollision(Transformable other)
        {
            if(other is Damageable damageable)
                damageable.Health.TakeDamage(_damage);   
        }
    }
}