using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class Enemy : AutoTargetCharacter
    {
        public Enemy(Vector3 position, Quaternion rotation, 
            int healthValue, CharacterBank characterBank, float minAttackDistance, float maxAttackDistance) 
            : base(position, rotation, healthValue, characterBank, minAttackDistance, maxAttackDistance)
        {
            
        }

        protected override void DefineTeam()
        {
            _characterBank.AddCharacter(this);
            _targets = _characterBank.Allies;
        }
    }
}