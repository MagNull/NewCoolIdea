using Sources.Runtime.Models;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public abstract class PresentersFactory<T> : MonoBehaviour where T : Transformable
    {
        public void Create(T model)
        {
            var presenter = Instantiate(GetPrefab(model), model.Position, model.Rotation);
            presenter.Init(model);
        }

        protected abstract Presenter<T> GetPrefab(T model);
    }
}
