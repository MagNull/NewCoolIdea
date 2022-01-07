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
    public class CharacterCompositeRoot : MonoBehaviour
    {
        [SerializeField] private CharacterBank _bank;
        private NavMeshAgent _navMeshAgent;
        private CharacterPresenter _presenter;
        private Character _character;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _presenter = GetComponent<CharacterPresenter>();
            _character = new CommandableCharacter(transform.position, transform.rotation, 
                new Health(10), GetComponent<Outline>());
            _character.Init(_navMeshAgent, _bank);

            _presenter.Init(_character);
        }
    }
}