using Sources.Runtime.Models;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public abstract class PresentersFactory : MonoBehaviour
    {
        public void Create(Transformable model)
        {
            var presenter = Instantiate(GetPrefab(model), model.Position, model.Rotation);
            presenter.Init(model);
        }

        protected abstract Presenter<Transformable> GetPrefab(Transformable model);
    }
}
