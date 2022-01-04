using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    [Serializable]
    public class Health
    {
        public Action Died;
        [SerializeField] private int _value = 10;

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