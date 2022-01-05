using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Presenters
{
    public class EnemyPresentersFactory : MonoBehaviour
    {
        [SerializeField] private CharacterPresenter _testEnemyPrefab;
        [SerializeField] private CharacterBank _characterBank;

        public void Create(Character model)
        {
            var presenter = Instantiate(_testEnemyPrefab, model.Position, model.Rotation);
            model.Init(presenter.GetComponent<NavMeshAgent>(), presenter.GetComponent<Animator>(), _characterBank);
            presenter.Init(model);
        }
    }
}
