using System;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public abstract class Transformable
    {
        [field: SerializeField] public virtual Vector3 Position { get; private set; }
        [field: SerializeField] public virtual Quaternion Rotation { get; private set; }

        public event Action Moved;
        public event Action Rotated;
        public event Action Destroying;

        public Transformable(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
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