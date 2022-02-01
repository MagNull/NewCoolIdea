using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class RangeWeapon : Weapon
    {
        private readonly ProjectilesFactory _projectilesFactory;
        private readonly Transform _projectileOrigin;
        
        public RangeWeapon(int damage, float minAttackDistance, float maxAttackDistance, 
            ProjectilesFactory projectilesFactory, Transform projectileOrigin) 
            : base(damage, minAttackDistance, maxAttackDistance)
        {
            _projectileOrigin = projectileOrigin;
            _projectilesFactory = projectilesFactory;
        }

        public override void Attack(Damageable damageable)
        {
            if (damageable.IsAlive)
            {
                var projectile = new Projectile(_projectileOrigin.position,
                    Quaternion.identity, damageable, 20, _damage);
                projectile.Activate();
                _projectilesFactory.Create(projectile);
            }
        }
    }
}