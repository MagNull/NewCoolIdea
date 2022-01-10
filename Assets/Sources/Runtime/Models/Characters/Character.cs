using System;
using System.Collections.Generic;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    [Serializable]
    public class Character : Damageable, IUpdatable, IStateful
    {
        private float _minAttackDistance;
        private float _maxAttackDistance;
        private NavMeshAgent _navMeshAgent;     
        private Damageable _targetCharacter;
        private StateMachine _stateMachine;
        protected List<Character> _targets;
        
        public Action<State> StateChanged { get; set; }
        

        public Character(Vector3 position, Quaternion rotation, Health health, 
            float minAttackDistance, float maxAttackDistance) : base(position, rotation, health)
        {
            Health.Died += Die;
            _minAttackDistance = minAttackDistance;
            _maxAttackDistance = maxAttackDistance;
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

        public void AttackTarget() => _targetCharacter?.Health.TakeDamage(1);//TODO: Define what damage

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
            else if (target is Damageable targetCharacter
                && targetCharacter != this)
            {
                _targetCharacter = targetCharacter;
            }
        }
        
        private State[] GetStates()
        {
            var states = new State[4];
            Damageable GetTarget() => _targetCharacter;
            states[0] = new IdleState(_navMeshAgent, GetTarget, this, _minAttackDistance, _stateMachine);
            states[1] = new MoveState(_navMeshAgent, GetTarget, this, _minAttackDistance, _stateMachine);
            states[2] = new AttackState(_navMeshAgent, GetTarget, this, _maxAttackDistance, _stateMachine);
            states[3] = new DieState(_navMeshAgent, GetTarget, this, _minAttackDistance, _stateMachine);

            return states;
        }

        protected virtual void Die()
        {
            _stateMachine.ChangeState<DieState>(); 
            _navMeshAgent.enabled = false;
        }
        
        protected virtual void DefineTeam(CharacterBank characterBank)
        {
            _targets = characterBank.Enemies;
            characterBank.Allies.Add(this);
            Health.Died += () => characterBank.Allies.Remove(this);
        }
    }
}