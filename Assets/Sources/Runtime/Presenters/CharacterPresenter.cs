using Sources.Runtime.Models;
using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Presenters
{
    [SelectionBase]
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class CharacterPresenter : Presenter<Character>
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private readonly int _attackTrigger = Animator.StringToHash("Attack");
        private readonly int _moveTrigger = Animator.StringToHash("Move");
        private readonly int _idleTrigger = Animator.StringToHash("Idle");
        private readonly int _dieTrigger = Animator.StringToHash("Die");
        private readonly int _warriorSpinTrigger = Animator.StringToHash("Warrior Spin");
        private readonly int _rangerMultishotTrigger = Animator.StringToHash("Ranger Multishot");

        public override void Init(Character model)
        {
            base.Init(model);
            Model.Init();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updatePosition = false;
        }

        public void AttackTarget()
        {
            Model.AttackTarget();   
        }

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

        protected override void Update()
        {
            base.Update();
            if (Model.IsAlive)
            {
                MoveNavMeshAgentToTarget();
            }
            Model.MoveTo(_navMeshAgent.nextPosition);
        }

        private void MoveNavMeshAgentToTarget()
        {
            dynamic target = Model.GetTarget();
            if (target is Transformable targetTransformable)
            {
                _navMeshAgent.SetDestination(targetTransformable.Position);
            }
            else if (target is Vector3 targetPoint)
            {
                _navMeshAgent.SetDestination(targetPoint);
            }
        }

        private void OnStateChanged(State newState)
        {
            if (newState is AttackState)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetTrigger(_attackTrigger);
            }
            else if (newState is MoveState)
            {
                _navMeshAgent.isStopped = false;
                _animator.SetTrigger(_moveTrigger);
            }
            else if (newState is IdleState)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetTrigger(_idleTrigger);
            }
            else if (newState is DieState)
            {
                _navMeshAgent.enabled = false;
                _animator.SetTrigger(_dieTrigger);
            }
            else if (newState is WarriorSpinState)
            {
                _navMeshAgent.isStopped = false;
                _animator.SetTrigger(_warriorSpinTrigger);
            }
            else if (newState is RangerMultishotState)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetTrigger(_rangerMultishotTrigger);
            }
        }
    }
}