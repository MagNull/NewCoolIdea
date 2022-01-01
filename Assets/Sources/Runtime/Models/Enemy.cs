using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models
{
    public class Enemy : AutoTargetCharacter
    {
        public Enemy(Vector3 position, Quaternion rotation, NavMeshAgent navMeshAgent, 
            Health health, CharacterBank bank) : base(position, rotation, navMeshAgent, health, bank)
        {
            _targets = bank.Allies;
        }
    }
}