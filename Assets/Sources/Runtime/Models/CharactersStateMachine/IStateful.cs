using System;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public interface IStateful
    {
        public event Action<State> StateChanged;
    }
}