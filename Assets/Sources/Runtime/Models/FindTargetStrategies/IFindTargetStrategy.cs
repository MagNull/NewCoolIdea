﻿using System.Collections.Generic;
using Sources.Runtime.Models.Characters;

namespace Sources.Runtime.Models.FindTargetStrategies
{
    public interface IFindTargetStrategy
    {
        Character GetTarget(List<Character> characters, Character originCharacter);
    }
}