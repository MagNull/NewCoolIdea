using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class Enemy : AutoTargetCharacter
    {
        public Enemy(Vector3 position, Quaternion rotation, 
            int healthValue, CharacterBank characterBank, StateMachine stateMachine) 
            : base(position, rotation, healthValue, characterBank, stateMachine)
        {
            
        }

        protected override void DefineTeam()
        {
            _characterBank.AddCharacter(this);
            _targets = _characterBank.Allies;
        }
    }
}