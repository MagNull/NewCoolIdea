using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public class DamageDealerPresenter : Presenter<DamageDealer>
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Presenter<Character> presenter))
                Model.OnCollision(presenter.Model);
        }
    }
}