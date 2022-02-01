namespace Sources.Runtime.Models
{
    public abstract class Weapon
    {
        protected int _damage;
        protected float _minAttackDistance;
        protected float _maxAttackDistance;
        
        public float MinAttackDistance => _minAttackDistance;
        public float MaxAttackDistance => _maxAttackDistance;

        protected Weapon(int damage, float minAttackDistance, float maxAttackDistance)
        {
            _damage = damage;
            _minAttackDistance = minAttackDistance;
            _maxAttackDistance = maxAttackDistance;
        }
        
        public abstract void Attack(Damageable damageable);
    }
}