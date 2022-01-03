using System;
using Sources.Runtime.Models.Characters;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public abstract class State
    {
        protected NavMeshAgent _navMeshAgent;
        protected Func<Character> _getTarget;
        protected Character _character;
        protected StateMachine _stateMachine;

        public State(NavMeshAgent navMeshAgent, Func<Character> getTarget, Character character,
            StateMachine stateMachine)
        {
            _navMeshAgent = navMeshAgent;
            _getTarget = getTarget;
            _character = character;
            _stateMachine = stateMachine;
        }
        
        public abstract void Enter();

        public abstract void Exit();

        public abstract void LogicUpdate();
    }
}