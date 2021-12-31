using Sources.Runtime.Models;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public abstract class Presenter<T> : MonoBehaviour where T : Transformable
    {
        [SerializeField] private T _model;

        public T Model => _model;

        public virtual void Init(T model, params object[] another)
        {
            _model = model;
            enabled = true;
        
            OnMoved();
            OnRotated();
        }

        protected virtual void OnEnable()
        {
            _model.Moved += OnMoved;
            _model.Rotated += OnRotated;
            _model.Destroying += OnDestroying;
        }

        protected virtual void OnDisable()
        {
            _model.Moved -= OnMoved;
            _model.Rotated -= OnRotated;
            _model.Destroying -= OnDestroying;
        }

        protected virtual void OnMoved()
        {
            transform.position = _model.Position;
        }

        protected virtual void OnRotated()
        {
            transform.rotation = _model.Rotation;
        }
    
        private void OnDestroying()
        {
            Destroy(gameObject);
        }

        protected void DestroyCompose()
        {
            _model.Destroy();
        }
    }
}