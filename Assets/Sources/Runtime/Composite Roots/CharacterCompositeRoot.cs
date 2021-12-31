using Sources.Runtime.Models;
using Sources.Runtime.Presenters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Composite_Roots
{
    [RequireComponent(
        typeof(NavMeshAgent), 
        typeof(CharacterPresenter), 
        typeof(Outline))]
    public class CharacterCompositeRoot : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private CharacterPresenter _presenter;
        private Outline _outline;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _presenter = GetComponent<CharacterPresenter>();
            _outline = GetComponent<Outline>();
            _presenter.Init(new CommandableCharacter(transform.position, transform.rotation, _navMeshAgent, _outline));
        }
    }
}