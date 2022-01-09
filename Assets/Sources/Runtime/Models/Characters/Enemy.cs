using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class Enemy : AutoTargetCharacter
    {
        public Enemy(Vector3 position, Quaternion rotation, 
            Health health, float minAttackDistance,float maxAttackDistance) 
            : base(position, rotation, health, minAttackDistance, maxAttackDistance)
        {
            
        }

        protected override void DefineTeam(CharacterBank characterBank)
        {
            _targets = characterBank.Allies;
            characterBank.Enemies.Add(this);
        }
    }
}