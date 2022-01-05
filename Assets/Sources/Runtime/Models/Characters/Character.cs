using System;
using System.Collections.Generic;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    [Serializable]
    public class Character : Transformable, IUpdatable
    {
        [SerializeField] private Health _health;
        private float _attackDistance = 3;
        private NavMeshAgent _navMeshAgent;
        private Character _targetCharacter;
        private StateMachine _stateMachine;
        protected List<Character> _targets;
        
        public Character(Vector3 position, Quaternion rotation, Health health) : base(position, rotation)
        {
            _health = health;
        }

        protected virtual void DefineTeam(CharacterBank characterBank)
        {
            _targets = characterBank.Enemies;
            characterBank.Allies.Add(this);
        }

        public void Init(NavMeshAgent navMeshAgent, Animator animator, CharacterBank bank)
        {
            DefineTeam(bank);
            _navMeshAgent = navMeshAgent;
            _stateMachine = new StateMachine();
            var states = GetStates(animator);
            _stateMachine.Init(states, states[0]);
        }

        private State[] GetStates(Animator animator)
        {
            var states = new State[3];
            states[0] = new IdleState(_navMeshAgent, GetTarget, this, _stateMachine, animator);
            states[1] = new MoveState(_navMeshAgent, GetTarget, this, _stateMachine, animator);
            states[2] = new AttackState(_navMeshAgent, GetTarget, this, _stateMachine, animator);

            return states;
        }

        public float AttackDistance => _attackDistance;

        public void TakeDamage(int damage) => _health.TakeDamage(damage);
        
        protected void SetTarget(object target)
        {
            _targetCharacter = null;
            if (target is Vector3 targetPos)
                _navMeshAgent.SetDestination(targetPos);
            if (target is Character targetCharacter)
                _targetCharacter = targetCharacter;
        }
        
        public virtual void Update(float deltaTime)
        {
            MoveTo(_navMeshAgent.nextPosition);
            _stateMachine.Update(deltaTime);
        }

        private Character GetTarget() => _targetCharacter;
        
    }
}