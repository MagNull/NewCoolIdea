using System;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    [RequireComponent(typeof(Animator))]
    public class CharacterPresenter : Presenter<Character>
    {
        private Animator _animator;
        private readonly int _attackTrigger = Animator.StringToHash("Attack");
        private readonly int _moveTrigger = Animator.StringToHash("Move");
        private readonly int _idleTrigger = Animator.StringToHash("Idle");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            if(newState is AttackState)
                _animator.SetTrigger(_attackTrigger);
            else if(newState is MoveState)
                _animator.SetTrigger(_moveTrigger);
            else if(newState is IdleState)
                _animator.SetTrigger(_idleTrigger);
        }
    }
}