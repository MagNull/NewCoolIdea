using System;
using Sources.External.QuickOutline.Scripts;
using Sources.Runtime.Models;
using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Models.CharactersStateMachine;
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
        [SerializeField]
        private CharacterClass _characterClass;
        private CharacterPresenter _presenter;
        private Character _character;

        enum CharacterClass
        {
            WARRIOR,
            RANGER,
            ENCHANTER
        }

        private void Start()
        {
            _presenter = GetComponent<CharacterPresenter>();
            
            Weapon weapon;
            if (_isRange)
                weapon = new RangeWeapon(1, _minAttackDistance, _maxAttackDistance,
                    _projectilesFactory, _projectileOrigin);
            else
                weapon = new MeleeWeapon(1, _minAttackDistance, _maxAttackDistance,
                    GetComponentInChildren<DamageDealerPresenter>().Model);
            
            var stateMachine = new StateMachine();
            _character = new CommandableCharacter(transform.position, transform.rotation,
                _startHealth, _bank, stateMachine,
                GetComponent<Outline>())
                .BindWeapon(weapon);
            _character.BindAbilities(CreateAbilities(stateMachine, weapon));
            
            _presenter.Init(_character);
        }

        private Ability[] CreateAbilities(StateMachine stateMachine, Weapon weapon) => 
            _characterClass switch
        {
            CharacterClass.WARRIOR => new Ability[]
            {
                new WarriorSpinState(_character.GetTarget, _character, stateMachine, 2, weapon)
            },
            CharacterClass.RANGER => new Ability[]
            {
                
            },
            CharacterClass.ENCHANTER => new Ability[]
            {
                
            },
            _ => new Ability[]{}
        };
    }
    
}