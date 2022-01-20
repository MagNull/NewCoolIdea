using System.Collections.Generic;
using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime
{
    public class CharacterBank : MonoBehaviour
    {
        private  List<Character> _allies = new List<Character>();
        private  List<Character> _enemies = new List<Character>();

        public IReadOnlyList<Damageable> Allies => _allies;
        public IReadOnlyList<Damageable> Enemies => _enemies;

        public void AddCharacter(Character character)
        {
            var teamList = character is Enemy ? _enemies : _allies;
            teamList.Add(character);
            character.Health.Died += () => teamList.Remove(character);
        }
    }
}
