using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models
{
    public class CommandableCharacter : Character
    {
        public Action Selected;
        public Action Deselected;

        public CommandableCharacter(Vector3 position, Quaternion rotation, 
            NavMeshAgent navMeshAgent, Outline outline) : base(position, rotation, navMeshAgent)
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