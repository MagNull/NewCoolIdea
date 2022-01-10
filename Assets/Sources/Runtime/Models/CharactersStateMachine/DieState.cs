using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class DieState : State
    {
        public DieState(NavMeshAgent navMeshAgent, Func<Damageable> getTarget, 
            Transformable characterTransformable, float attackDistance, StateMachine stateMachine)
            : base(navMeshAgent, getTarget, characterTransformable, attackDistance, stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log(_navMeshAgent.name + " died.");
        }

        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}