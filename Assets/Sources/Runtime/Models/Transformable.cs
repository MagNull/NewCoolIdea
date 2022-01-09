using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public abstract class Transformable
    {
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }
        public Vector3 Forward => Quaternion.Euler(0, Rotation.eulerAngles.y, 0) * Vector3.forward;
        
        public event Action Moved;
        public event Action Rotated;
        public event Action Destroying;

        public Transformable(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public Vector3 TransformDirection(Vector3 direction)
        {
            Quaternion angle = Quaternion.FromToRotation(Vector3.forward, Forward);
            return angle * direction;
        }

        public void LookAt(object target)
        {
            if(target is Transformable targetTransformable)
                Rotation = Quaternion.LookRotation(targetTransformable.Position - Position);
            else if(target is Vector3 targetVector)
                Rotation = Quaternion.LookRotation(targetVector - Position);
            Rotated?.Invoke();
        }
        
        public void RotateTo(Vector3 rotation)
        {
            Rotation = Quaternion.Euler(rotation);
            Rotated?.Invoke();
        }

        public void MoveTo(Vector3 position)
        {
            Position = position;
            Moved?.Invoke();
        }

        public void Destroy()
        {
            Destroying?.Invoke();
        }
    }
}