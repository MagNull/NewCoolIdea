namespace Sources.Runtime.Models
{
    public class MeleeWeapon : Weapon, IActivable
    {
        private readonly IActivable _damageDealer;

        public MeleeWeapon(int damage, float minAttackDistance, float maxAttackDistance, DamageDealer damageDealer) 
            : base(damage, minAttackDistance, maxAttackDistance)
        {
            _damageDealer = damageDealer;
        }

        public override void Attack(Damageable damageable)
        {
            damageable.Health.TakeDamage(_damage);
        }

        public void Activate() => _damageDealer.Activate();

        public void Deactivate() => _damageDealer.Deactivate();
    }
}