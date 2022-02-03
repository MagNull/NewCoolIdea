using System;
using Sources.Runtime.Models.CharactersStateMachine;
using Sources.Runtime.Presenters;

namespace Sources.Runtime.Models.Abilities
{
    public class RangerMultishotState : Ability
    {
        private ProjectilesFactory _projectilesFactory;
        
        public RangerMultishotState(Func<dynamic> getTarget, Transformable characterTransformable,
            StateMachine stateMachine) : base(getTarget, characterTransformable, stateMachine)
        {
        }
    }
}