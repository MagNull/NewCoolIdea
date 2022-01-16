using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class Enemy : AutoTargetCharacter
    {
        public Enemy(Vector3 position, Quaternion rotation, 
            Health health, CharacterBank characterBank, float minAttackDistance, float maxAttackDistance) 
            : base(position, rotation, health, characterBank, minAttackDistance, maxAttackDistance)
        {
            
        }

        protected override void DefineTeam()
        {
            _characterBank.AddCharacter(this);
            _targets = _characterBank.Allies;
        }
    }
}