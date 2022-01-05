using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class Enemy : AutoTargetCharacter
    {
        public Enemy(Vector3 position, Quaternion rotation, 
            Health health) : base(position, rotation, health)
        {
            
        }

        protected override void DefineTeam(CharacterBank characterBank)
        {
            _targets = characterBank.Allies;
            characterBank.Enemies.Add(this);
        }
    }
}