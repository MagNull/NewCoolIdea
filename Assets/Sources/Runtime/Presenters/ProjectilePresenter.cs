using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public class ProjectilePresenter : Presenter<Projectile>
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Presenter<Character> presenter))
                Model.OnCollision(presenter.Model);
        }
    }
}