using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Presenters
{
    [SelectionBase]
    [RequireComponent(typeof(Animator))]
    public class CharacterPresenter : Presenter<Character>
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private readonly int _attackTrigger = Animator.StringToHash("Attack");
        private readonly int _moveTrigger = Animator.StringToHash("Move");
        private readonly int _idleTrigger = Animator.StringToHash("Idle");
        private readonly int _dieTrigger = Animator.StringToHash("Die");

        public override void Init(Character model)
        {
            base.Init(model);
            Model.Init(_navMeshAgent);
        }

        public void AttackTarget()
        {
            Model.AttackTarget();   
        }

        public void DisableNavMeshAgent() => _navMeshAgent.enabled = false;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Model.StateChanged += OnStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Model.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(State newState)
        {
            _navMeshAgent.enabled = true;
            if(newState is AttackState)
                _animator.SetTrigger(_attackTrigger);
            else if(newState is MoveState)
                _animator.SetTrigger(_moveTrigger);
            else if(newState is IdleState)
                _animator.SetTrigger(_idleTrigger);
            else if(newState is DieState)
                _animator.SetTrigger(_dieTrigger);
            else if(newState is AbilityCastState ability)
                _animator.SetTrigger("Ability " + ability.AbilityNumber);
        }
    }
}