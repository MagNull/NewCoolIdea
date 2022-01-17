using Sources.External.QuickOutline.Scripts;
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
        [SerializeField] 
        private bool _isRange;
        [SerializeField] 
        private Transform _projectileOrigin;
        [SerializeField] 
        private ProjectilesFactory _projectilesFactory;
        
        [SerializeField] 
        private CharacterBank _bank;
        [SerializeField] 
        private float _minAttackDistance = .5f;
        [SerializeField] 
        private float _maxAttackDistance = .5f;
        [SerializeField] 
        private int _startHealth = 5;
        private CharacterPresenter _presenter;
        private Character _character;

        private void Awake()
        {
            _presenter = GetComponent<CharacterPresenter>();
            
            _character =  _isRange ? new RangeAttackCharacter(transform.position, transform.rotation, 
                    _startHealth, _bank, _minAttackDistance, _maxAttackDistance, _projectilesFactory, _projectileOrigin)
                : new Character(transform.position, transform.rotation, 
                    _startHealth, _bank, _minAttackDistance, _maxAttackDistance);
            
            _character = new CommandableCharacter(_character, _bank, GetComponent<Outline>());
            _presenter.Init(_character);
        }
    }
}