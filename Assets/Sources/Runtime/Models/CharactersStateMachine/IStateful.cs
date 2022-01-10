using System;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public interface IStateful
    {
        public Action<State> StateChanged { get; set; }
    }
}