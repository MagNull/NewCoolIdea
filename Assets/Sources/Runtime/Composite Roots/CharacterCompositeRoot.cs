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
        [SerializeField] private bool _isEnemy = false;
        [SerializeField] private CharacterBank _bank;
        private NavMeshAgent _navMeshAgent;
        private CharacterPresenter _presenter;
        private Outline _outline;
        private Character _character;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _presenter = GetComponent<CharacterPresenter>();
            _outline = GetComponent<Outline>();
            _character = _isEnemy ? 
                new Enemy(transform.position, transform.rotation, _navMeshAgent, new Health(10), _bank) : 
                new CommandableCharacter(transform.position, transform.rotation, _navMeshAgent, new Health(10), _outline);
            
            if (_character is Enemy)
                _bank.Enemies.Add(_character);
            else
                _bank.Allies.Add(_character);

            _presenter.Init(_character);
        }

        private void Update()
        {
            _character.Update();
        }
    }
}