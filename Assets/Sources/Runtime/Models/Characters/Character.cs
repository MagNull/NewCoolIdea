using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    [Serializable]
    public class Character : Transformable, IUpdatable
    {
        public Action<State> StateChanged;

        [SerializeField] private Health _health;
        private float _attackDistance = 3;
        private NavMeshAgent _navMeshAgent;
        private Character _targetCharacter;
        private StateMachine _stateMachine;
        protected List<Character> _targets;
        
        public bool IsAlive { get; private set; }
        
        public Character(Vector3 position, Quaternion rotation, Health health, float attackDistance) : base(position, rotation)
        {
            _health = health;
            IsAlive = true;
            _health.Died += Die;
            _attackDistance = attackDistance;
        }

        public void Init(NavMeshAgent navMeshAgent, CharacterBank bank)
        {
            DefineTeam(bank);
            _navMeshAgent = navMeshAgent;
            _navMeshAgent.updateRotation = false;
            _stateMachine = new StateMachine(this);
            var states = GetStates();
            _stateMachine.Init(states, states[0]);
        }

        public void TakeDamage(int damage) => _health.TakeDamage(damage);

        public void AttackTarget() => _targetCharacter?.TakeDamage(1);//TODO: Define what damage

        public virtual void Update(float deltaTime)
        {
            MoveTo(_navMeshAgent.nextPosition);
            _stateMachine.Update(deltaTime);
        }
        
        protected void SetTarget(object target)
        {
            _targetCharacter = null;
            if (target is Vector3 targetPos)
            {
                _navMeshAgent.SetDestination(targetPos);
                LookAt(target);
            }

            if (target is Character targetCharacter
                && targetCharacter != this)
            {
                _targetCharacter = targetCharacter;
            }
        }
        
        private State[] GetStates()
        {
            var states = new State[4];
            Character GetTarget() => _targetCharacter;
            states[0] = new IdleState(_navMeshAgent, GetTarget, this, _attackDistance, _stateMachine);
            states[1] = new MoveState(_navMeshAgent, GetTarget, this, _attackDistance, _stateMachine);
            states[2] = new AttackState(_navMeshAgent, GetTarget, this, _attackDistance, _stateMachine);
            states[3] = new DieState(_navMeshAgent, GetTarget, this, _attackDistance, _stateMachine);

            return states;
        }

        protected virtual void Die()
        {
            IsAlive = false;
            _navMeshAgent.enabled = false;
           _stateMachine.ChangeState<DieState>(); 
        }
        
        protected virtual void DefineTeam(CharacterBank characterBank)
        {
            _targets = characterBank.Enemies;
            characterBank.Allies.Add(this);
        }
    }
}