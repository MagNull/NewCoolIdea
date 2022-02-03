using System;
using System.Collections.Generic;
using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.CharactersStateMachine;
using Sources.Runtime.Presenters;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    [Serializable]
    public class Character : Damageable, IUpdatable, IStateful
    {
        public event Action<State> StateChanged;

        protected AbilityCast _abilityCast;
        protected IReadOnlyList<Damageable> _targets;
        protected readonly CharacterBank _characterBank;
        
        private dynamic _target;
        private StateMachine _stateMachine;
        private Weapon _weapon;
        
        public Character(Vector3 position, Quaternion rotation, int healthValue, CharacterBank characterBank) 
            : base(position, rotation, healthValue)
        {
            _characterBank = characterBank;
            
            _stateMachine = new StateMachine();
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

        public Character BindAbilities(Ability[] abilities)
        {
            _abilityCast = new AbilityCast(_stateMachine, abilities);
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
        }

        public void Init()
        {
            DefineTeam();
            var states = GetStates();
            _stateMachine.StateChanged += StateChanged;
            _stateMachine.Init(states, states[0]);
        }

        public dynamic GetTarget() => _target;

        protected virtual void SetTarget(dynamic target)
        {
            if(target is Transformable transformable &&
               transformable.Position == Position)
                return;
            _target = target;
        }

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
                this, _weapon.MinAttackDistance, _stateMachine);
            states[1] = new MoveState(GetTarget, 
                this, _weapon.MinAttackDistance, _stateMachine);
            states[2] = new AttackState(GetTarget, 
                this, GetWeapon, _stateMachine);
            states[3] = new DieState(GetTarget, this, _stateMachine);

            return states;
        }
    }
}