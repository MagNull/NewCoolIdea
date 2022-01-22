using System;

namespace Sources.Runtime.Models
{
    public class Health
    {
        public event Action Died;
        private int _value;

        public Health(int value)
        {
            _value = value;
        }

        public int Value => _value;

        public void TakeDamage(int damage)
        {
            _value -= damage;
            if(Value <= 0)
                Died?.Invoke();
        }
    }
}