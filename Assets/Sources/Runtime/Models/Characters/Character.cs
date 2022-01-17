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
        public virtual event Action<State> StateChanged;
        
        protected CharacterBank _characterBank;
        protected IReadOnlyList<Damageable> _targets;
        private float _minAttackDistance;
        private float _maxAttackDistance;
        private NavMeshAgent _navMeshAgent;
        private Damageable _targetCharacter;
        private StateMachine _stateMachine;

        public float MinAttackDistance => _minAttackDistance;
        public float MaxAttackDistance => _maxAttackDistance;

        public Character(Vector3 position, Quaternion rotation, int healthValue, CharacterBank characterBank, 
            float minAttackDistance, float maxAttackDistance) : base(position, rotation, healthValue)
        {
            _characterBank = characterBank;
            _minAttackDistance = minAttackDistance;
            _maxAttackDistance = maxAttackDistance;
        }

        public virtual void Init(NavMeshAgent navMeshAgent)
        {
            DefineTeam();
            _navMeshAgent = navMeshAgent;
            _navMeshAgent.updateRotation = false;
            _stateMachine = new StateMachine();
            var states = GetStates();
            _stateMachine.StateChanged += StateChanged;
            _stateMachine.Init(states, states[0]);
        }

        public virtual void AttackTarget()
        {
            _targetCharacter?.Health.TakeDamage(1);//TODO: Define what damage
        }

        public virtual void Update(float deltaTime)
        {
            MoveTo(_navMeshAgent.nextPosition);
            _stateMachine.Update(deltaTime);
        }

        public virtual void SetTarget(object target)
        {
            _targetCharacter = null;
            if (target is Vector3 targetPos)
            {
                _navMeshAgent.SetDestination(targetPos);
                LookAt(target);
            }
            else if (target is Damageable targetCharacter
                && targetCharacter.Position != Position)
            {
                _targetCharacter = targetCharacter;
            }
        }

        public override void Die()
        {
            base.Die();
            _stateMachine.ChangeState<DieState>();
            _navMeshAgent.enabled = false;
        }

        protected Damageable GetTarget() => _targetCharacter;

        protected virtual void DefineTeam()
        {
            _characterBank.AddCharacter(this);
            _targets = _characterBank.Allies;
        }

        private State[] GetStates()
        {
            var states = new State[4];
            states[0] = new IdleState(_navMeshAgent, GetTarget, this, MinAttackDistance, _stateMachine);
            states[1] = new MoveState(_navMeshAgent, GetTarget, this, MinAttackDistance, _stateMachine);
            states[2] = new AttackState(_navMeshAgent, GetTarget, this, MaxAttackDistance, _stateMachine);
            states[3] = new DieState(_navMeshAgent, GetTarget, this, MinAttackDistance, _stateMachine);

            return states;
        }
    }
}