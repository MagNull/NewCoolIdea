using System;
using Sources.External.QuickOutline.Scripts;
using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class CommandableCharacter : Character
    {
        public Action Selected;
        public Action Deselected;

        private CharacterControl _currentCommander;

        public CommandableCharacter(Vector3 position, Quaternion rotation, Health health, 
            Outline outline, float minAttackDistance, float maxAttackDistance) 
            : base(position, rotation, health, minAttackDistance, maxAttackDistance)
        {
            Selected += () => outline.enabled = true;
            Deselected += () => outline.enabled = false;
        }

        public void Select(CharacterControl commander)
        {
            if (IsAlive)
            {
                _currentCommander = commander;
                _currentCommander.Commanded += SetTarget;
                _currentCommander.SelectionCanceled += Deselect;
                Selected?.Invoke();
            }
        }

        private void Deselect(CharacterControl commander)
        {
            _currentCommander.Commanded -= SetTarget;
            _currentCommander.SelectionCanceled -= Deselect;
            _currentCommander = null;
            Deselected?.Invoke();
        }

        protected override void Die()
        {
            if (!(_currentCommander is null))
                Deselect(_currentCommander);
            base.Die();
        }
    }
}