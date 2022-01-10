using System;

namespace Sources.Runtime.Models
{
    public class Health
    {
        public Action Died;
        private int _value = 10;

        public Health(int value)
        {
            _value = value;
        }
        
        public void TakeDamage(int damage)
        {
            _value -= damage;
            if(_value <= 0)
                Died?.Invoke();
        }
    }
}