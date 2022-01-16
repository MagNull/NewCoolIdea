using System;
using Sources.Runtime.Models.Characters;
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
            if (_target is null)
            {
                Destroy();
            }
            else
            {
                Vector3 newPosition = 
                    Vector3.MoveTowards(Position, _target.Position, _speed * deltaTime);
                MoveTo(newPosition);
            }
            
        }

        public void OnCollision(Character character)
        {
            character.Health.TakeDamage(_damage);
            Destroy();
        }
    }
}