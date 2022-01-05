using System;
using Sources.Runtime.Models.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public abstract class State : IUpdatable
    {
        protected NavMeshAgent _navMeshAgent;
        protected Func<Character> _getTarget;
        protected Character _character;
        protected StateMachine _stateMachine;
        protected Animator _animator;
        protected int _animationTrigger;

        public State(NavMeshAgent navMeshAgent, Func<Character> getTarget, Character character,
            StateMachine stateMachine, Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _getTarget = getTarget;
            _character = character;
            _stateMachine = stateMachine;
            _animator = animator;
        }
        
        public abstract void Enter();

        public abstract void Exit();

        public abstract void LogicUpdate();
        public abstract void Update(float deltaTime);
    }
}