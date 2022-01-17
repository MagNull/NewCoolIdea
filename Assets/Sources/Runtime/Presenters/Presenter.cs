using Sources.Runtime.Models;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public abstract class Presenter<T> : MonoBehaviour where T : Transformable
    {
        public T Model => _model;
        
        [SerializeField] 
        private T _model;
        private IUpdatable _updatable;

        public virtual void Init(T model)
        {
            _model = model;
            enabled = true;
            
            if (model is IUpdatable updatable)
                _updatable = updatable;
        
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

        private void OnMoved()
        {
            transform.position = _model.Position;
        }

        private void OnRotated()
        {
            transform.rotation = _model.Rotation;
        }
    
        private void OnDestroying()
        {
            Destroy(gameObject);
        }

        private void Update() => _updatable?.Update(Time.deltaTime);

        private void DestroyCompose()
        {
            _model.Destroy();
        }
    }
}