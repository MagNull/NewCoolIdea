using System.Collections.Generic;
using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime.Models.FindTargetStrategies
{
    public class FindNearestStrategy : IFindTargetStrategy
    {
        public Damageable GetTarget(IReadOnlyList<Damageable> characters, Character originCharacter)
        {
            var nearest = characters[0];
            foreach (var character in characters)
            {
                if (IsNearest(originCharacter.Position, character.Position, nearest.Position)
                    && character != originCharacter)
                {
                    nearest = character;
                }
            }

            return nearest;
        }

        private bool IsNearest(Vector3 origin, Vector3 point1, Vector3 point2)
        {
            return Vector3.SqrMagnitude(origin - point1) < Vector3.SqrMagnitude(origin - point2);
        }
    }
}