using System;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    public abstract class CharacterDecorator : Character
    {
        public override Vector3 Position => _baseCharacter.Position;
        public override Quaternion Rotation => _baseCharacter.Rotation;
        public override bool IsAlive => _baseCharacter.IsAlive;

        public override Health Health => _baseCharacter is null ? base.Health : _baseCharacter.Health;

        public override event Action<State> StateChanged
        {
            add => _baseCharacter.StateChanged += value;
            remove => _baseCharacter.StateChanged -= value;
        }
        
        protected Character _baseCharacter;

        protected CharacterDecorator(Character baseCharacter, CharacterBank characterBank) 
            : base(baseCharacter.Position, baseCharacter.Rotation, baseCharacter.Health.Value, 
                characterBank, baseCharacter.MinAttackDistance, baseCharacter.MaxAttackDistance)
        {
            _baseCharacter = baseCharacter;
            _baseCharacter.Moved += () => MoveTo(_baseCharacter.Position);
            _baseCharacter.Rotated += () => RotateTo(_baseCharacter.Rotation.eulerAngles);
            _baseCharacter.Destroying += Destroy;
        }

        public override void Init(NavMeshAgent navMeshAgent) => _baseCharacter.Init(navMeshAgent);

        public override void Update(float deltaTime) => _baseCharacter.Update(deltaTime);

        public override void AttackTarget() => _baseCharacter.AttackTarget();

        public override void SetTarget(object target) => _baseCharacter.SetTarget(target);

        public override void Die() => _baseCharacter.Die();
    }
}