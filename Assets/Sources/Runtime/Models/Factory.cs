using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Presenters
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private CharacterPresenter _testEnemyPrefab;
        [SerializeField] private ProjectilePresenter _projectilePresenterPrefab;
        [SerializeField] private CharacterBank _characterBank;

        public void Create(Transformable model)
        {
            if (model is Enemy character)
            {
                var presenter = Instantiate(_testEnemyPrefab, model.Position, model.Rotation);
                presenter.Init(character);
            }
            else if (model is Projectile projectile)
            {
                var presenter = Instantiate(_projectilePresenterPrefab, model.Position, model.Rotation);
                presenter.Init(projectile);
            }
        }
    }
}
