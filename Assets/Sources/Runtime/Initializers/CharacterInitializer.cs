using Sources.External.QuickOutline.Scripts;
using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Presenters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Composite_Roots
{
    [RequireComponent(
        typeof(NavMeshAgent),
        typeof(CharacterPresenter),
        typeof(Outline))]
    [RequireComponent(typeof(Animator))]
    public class CharacterInitializer : MonoBehaviour
    {
        [SerializeField] private CharacterBank _bank;
        [SerializeField] private float _minAttackDistance = .5f;
        [SerializeField] private float _maxAttackDistance = .5f;
        private CharacterPresenter _presenter;
        private Character _character;

        private void Awake()
        {
            _presenter = GetComponent<CharacterPresenter>();
            _character = new CommandableCharacter(transform.position, transform.rotation, 
                new Health(5), _bank, GetComponent<Outline>(), _minAttackDistance, _maxAttackDistance);
            _presenter.Init(_character);
        }
    }
}