﻿using System.Collections.Generic;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class FindNearestStrategy : IFindTargetStrategy
    {
        public Character GetTarget(List<Character> characters, Character originCharacter)
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