using System;
using Sources.External.QuickOutline.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    public class CommandableCharacter : Character
    {
        public Action Selected;
        public Action Deselected;

        public CommandableCharacter(Vector3 position, Quaternion rotation, Health health, 
            Outline outline, float attackDistance) 
            : base(position, rotation, health, attackDistance)
        {
            Selected += () => outline.enabled = true;
            Deselected += () => outline.enabled = false;
        }

        public void Select(CharacterControl commander)
        {
            commander.Commanded += SetTarget;
            commander.SelectionCanceled += Deselect;
            Selected?.Invoke();
        }

        private void Deselect(CharacterControl commander)
        {
            commander.Commanded -= SetTarget;
            commander.SelectionCanceled -= Deselect;
            Deselected?.Invoke();
        }
    }
}