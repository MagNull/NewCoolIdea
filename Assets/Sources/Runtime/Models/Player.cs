using System;
using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class Player : Transformable
    {
        private float _health = 10;

        public Player(Vector3 position, Quaternion rotation) : base(position, rotation)
        {
            
        }
    }
}