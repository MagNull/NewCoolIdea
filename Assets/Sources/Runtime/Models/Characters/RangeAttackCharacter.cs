using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class RangeAttackCharacter : Character
    {
        private readonly ProjectilesFactory _projectilesFactory;
        private readonly Transform _projectileOrigin;
        
        public RangeAttackCharacter(Vector3 position, Quaternion rotation, int healthValue,
            CharacterBank characterBank, 
            float minAttackDistance, float maxAttackDistance, ProjectilesFactory projectilesFactory, 
            Transform projectileOrigin)
            : base(position, rotation, healthValue, characterBank, minAttackDistance, maxAttackDistance)
        {
            _projectilesFactory = projectilesFactory;
            _projectileOrigin = projectileOrigin;
        }

        public override void AttackTarget()
        {
            var target = GetTargetCharacter();
            if (target is Damageable {IsAlive: true} damageableTarget)
            {
                var projectile = new Projectile(_projectileOrigin.position,
                    Quaternion.identity, damageableTarget, 20, 1);
                _projectilesFactory.Create(projectile);
            }
        }
    }
}