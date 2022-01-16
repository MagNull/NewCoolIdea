using Sources.Runtime.Models;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public class ProjectilePresenter : Presenter<Projectile>
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Presenter<Transformable> presenter))
                Model.OnCollision(presenter.Model);
        }
    }
}