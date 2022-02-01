using System;
using System.Collections.Generic;
using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    [Serializable]
    public class Character : Damageable, IUpdatable, IStateful
    {
        public virtual event Action<State> StateChanged;

        protected AbilityCast _abilityCast;
        protected IReadOnlyList<Damageable> _targets;
        protected readonly CharacterBank _characterBank;

        private NavMeshAgent _navMeshAgent;
        private dynamic _target;
        private StateMachine _stateMachine;
        private Weapon _weapon;
        

        public Character(Vector3 position, Quaternion rotation, int healthValue, CharacterBank characterBank) 
            : base(position, rotation, healthValue)
        {
            _characterBank = characterBank;
        }

        public void AttackTarget()
        {
            if(_target is Damageable damageable)
                _weapon.Attack(damageable);
        }

        public Character BindWeapon(Weapon weapon)
        {
            _weapon = weapon;
            return this;
        }

        public virtual void Update(float deltaTime)
        {
            _abilityCast.Update(deltaTime);
            _stateMachine.Update(deltaTime);
        }

        public override void Die()
        {
            base.Die();
            _stateMachine.ChangeState<DieState>();
            _navMeshAgent.enabled = false;
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

            _abilityCast = new AbilityCast(_stateMachine, 
                new []
                {
                    new Ability("Warrior Spin", 2, 2, true)
                });
        }

        protected virtual void SetTarget(dynamic target)
        {
            if(target is Transformable transformable &&
               transformable.Position == Position)
                return;
            _target = target;
        }

        protected dynamic GetTarget() => _target;

        protected virtual void DefineTeam()
        {
            _characterBank.AddCharacter(this);
            _targets = _characterBank.Allies;
        }

        private State[] GetStates()
        {
            Weapon GetWeapon() => _weapon;
            var states = new State[5];
            states[0] = new IdleState(GetTarget, 
                this, GetWeapon, _stateMachine);
            states[1] = new MoveState(_navMeshAgent, GetTarget, 
                this, GetWeapon, _stateMachine);
            states[2] = new AttackState(GetTarget, 
                this, GetWeapon, _stateMachine);
            states[3] = new DieState(GetTarget, 
                this, GetWeapon, _stateMachine);
            states[4] = new AbilityCastState(_navMeshAgent, GetTarget, 
                this, GetWeapon, _stateMachine);

            return states;
        }
    }
}