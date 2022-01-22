using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class Projectile : DamageDealer, IUpdatable
    {
        private readonly float _speed;
        private readonly Damageable _target;

        public Projectile(Vector3 position, Quaternion rotation, 
            Damageable target, float speed, int damage) 
            : base(position, rotation, damage)
        {
            _target = target;
            _speed = speed;
        }

        public override void OnCollision(Character character)
        {
            base.OnCollision(character);
            Destroy();
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
                newPosition.y = Position.y;
                MoveTo(newPosition);
            }
        }
    }
}