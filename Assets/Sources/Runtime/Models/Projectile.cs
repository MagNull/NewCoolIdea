using UnityEngine;

namespace Sources.Runtime.Models
{
    public class Projectile : DamageDealer, IUpdatable
    {
        private readonly float _speed;
        private readonly dynamic _target;

        public Projectile(Vector3 position, Quaternion rotation, 
            dynamic target, float speed, int damage) 
            : base(position, rotation, damage)
        {
            _target = target;
            _speed = speed;
        }

        public override void OnCollision(Damageable character)
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
                Vector3 newPosition = new Vector3();
                if(_target is Damageable damageable)
                {
                    newPosition = 
                        Vector3.MoveTowards(Position, damageable.Position, _speed * deltaTime);
                }
                else if (_target is Vector3 direction)
                {
                    newPosition = direction.normalized * _speed * deltaTime + Position;
                }
                newPosition.y = Position.y;
                MoveTo(newPosition);
            }
        }
    }
}