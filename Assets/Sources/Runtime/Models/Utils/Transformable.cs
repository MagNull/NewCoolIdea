using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public abstract class Transformable
    {
        [field: SerializeField] 
        public virtual Vector3 Position { get; private set; }
        [field: SerializeField] 
        public virtual Quaternion Rotation { get; private set; }

        public event Action Moved;
        public event Action Rotated;
        public event Action Destroying;

        protected Transformable(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public void LookAt(object target)
        {
            Vector3 targetPosition = new Vector3();
            if (target is Transformable targetTransformable)
                targetPosition = targetTransformable.Position;
            else if (target is Vector3 targetVector)
                targetPosition = targetVector;
            
            targetPosition.y = Position.y;
            var lookRotationVector = targetPosition - Position;
            if(lookRotationVector == Vector3.zero)
                return;
            
            Rotation = Quaternion.LookRotation(targetPosition - Position);
            Rotated?.Invoke();
        }

        public void MoveTo(Vector3 position)
        {
            Position = position;
            Moved?.Invoke();
        }

        protected void RotateTo(Vector3 rotation)
        {
            Rotation = Quaternion.Euler(rotation);
            Rotated?.Invoke();
        }

        public void Destroy()
        {
            Destroying?.Invoke();
        }
    }
}