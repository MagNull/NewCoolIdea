using System.Collections.Generic;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public interface IFindTargetStrategy
    {
        Character GetTarget(List<Character> characters, Character originCharacter);
    }
}