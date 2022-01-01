using System;
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
        private Character _character;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _presenter = GetComponent<CharacterPresenter>();
            _outline = GetComponent<Outline>();
            _character = new CommandableCharacter(transform.position, transform.rotation, _navMeshAgent, _outline);
            _presenter.Init(_character);
        }

        private void Update()
        {
            _character.Update();
        }
    }
}