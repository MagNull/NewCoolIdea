using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class Enemy : AutoTargetCharacter
    {
        public Enemy(Vector3 position, Quaternion rotation, 
            int healthValue, CharacterBank characterBank) 
            : base(position, rotation, healthValue, characterBank)
        {
            
        }

        protected override void DefineTeam()
        {
            _characterBank.AddCharacter(this);
            _targets = _characterBank.Allies;
        }
    }
}