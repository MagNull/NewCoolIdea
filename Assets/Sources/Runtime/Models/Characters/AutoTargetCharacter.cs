using Sources.Runtime.Models.Abilities;
using Sources.Runtime.Models.CharactersStateMachine;
using Sources.Runtime.Models.FindTargetStrategies;
using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public abstract class AutoTargetCharacter : Character
    {
        private readonly IFindTargetStrategy _findTargetStrategy;

        public AutoTargetCharacter(Vector3 position, Quaternion rotation, int healthValue,
            CharacterBank characterBank, StateMachine stateMachine) 
            : base(position, rotation, healthValue, characterBank, stateMachine)
        {
            _findTargetStrategy = new FindNearestStrategy();
        }

        public override void Update(float deltaTime)
        {
            SetTarget(_findTargetStrategy.GetTarget(_targets, this));
            base.Update(deltaTime);
        }
    }
}