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

        protected IReadOnlyList<Damageable> _targets;
        protected readonly CharacterBank _characterBank;
        
        private NavMeshAgent _navMeshAgent;
        private float _minAttackDistance;
        private float _maxAttackDistance;
        private dynamic _target;
        private StateMachine _stateMachine;

        public float MinAttackDistance => _minAttackDistance;
        public float MaxAttackDistance => _maxAttackDistance;

        public StateMachine StateMachine => _stateMachine;

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
            StateMachine.StateChanged += StateChanged;
            StateMachine.Init(states, states[0]);
        }

        public virtual void AttackTarget()
        {
            _target?.Health.TakeDamage(1);//TODO: Define what damage
        }

        public virtual void Update(float deltaTime)
        {
            StateMachine.Update(deltaTime);
        }

        public virtual void SetTarget(dynamic target)
        {
            if(target is Transformable transformable &&
               transformable.Position == Position)
                return;
            _target = target;
        }

        public override void Die()
        {
            base.Die();
            StateMachine.ChangeState<DieState>();
            _navMeshAgent.enabled = false;
        }

        protected dynamic GetTargetCharacter() => _target;

        protected virtual void DefineTeam()
        {
            _characterBank.AddCharacter(this);
            _targets = _characterBank.Allies;
        }

        private State[] GetStates()
        {
            var states = new State[5];
            states[0] = new IdleState(GetTargetCharacter, 
                this, MinAttackDistance, StateMachine);
            states[1] = new MoveState(_navMeshAgent, GetTargetCharacter, 
                this, MinAttackDistance, StateMachine);
            states[2] = new AttackState(GetTargetCharacter, 
                this, MaxAttackDistance, StateMachine);
            states[3] = new DieState(GetTargetCharacter, 
                this, MinAttackDistance, StateMachine);
            states[4] = new AbilityCastState(GetTargetCharacter, 
                this, MinAttackDistance, StateMachine);

            return states;
        }
    }
}