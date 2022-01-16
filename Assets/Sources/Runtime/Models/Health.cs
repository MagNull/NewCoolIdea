using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    [Serializable]
    public class Health
    {
        public event Action Died;
        [SerializeField] private int _value = 10;

        public Health(int value)
        {
            _value = value;
        }

        public int Value => _value;

        public void TakeDamage(int damage)
        {
            _value = Value - damage;
            if(Value <= 0)
                Died?.Invoke();
        }
    }
}