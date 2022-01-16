using System.Collections.Generic;
using Sources.Runtime.Models.Characters;

namespace Sources.Runtime.Models.FindTargetStrategies
{
    public interface IFindTargetStrategy
    {
        Damageable GetTarget(IReadOnlyList<Damageable> characters, Character originCharacter);
    }
}