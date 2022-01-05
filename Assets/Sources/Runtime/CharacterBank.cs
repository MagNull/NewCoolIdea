using System.Collections.Generic;
using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime
{
    public class CharacterBank : MonoBehaviour
    {
        public List<Character> Allies = new List<Character>();
        public List<Character> Enemies = new List<Character>();
    }
}
