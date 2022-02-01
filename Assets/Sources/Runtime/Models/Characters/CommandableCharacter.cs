using System;
using Sources.External.QuickOutline.Scripts;
using UnityEngine;

namespace Sources.Runtime.Models.Characters
{
    public class CommandableCharacter : Character
    {
        public event Action Selected;
        public event Action Deselected;

        private CharacterControl _currentCommander;
        
        public CommandableCharacter(Vector3 position, Quaternion rotation, int healthValue, 
            CharacterBank characterBank, Outline outline) 
            : base(position, rotation, healthValue, characterBank)
        {
            Selected += () => outline.enabled = true;
            Deselected += () => outline.enabled = false;
        }
        
        public void Select(CharacterControl commander)
        {
            if (IsAlive)
            {
                _currentCommander = commander;
                _currentCommander.TargetingCommanded += SetTarget;
                _currentCommander.SelectionCanceled += Deselect;
                _currentCommander.AbilityUseTried += _abilityCast.OnAbilityUseTried;
                Selected?.Invoke();
            }
        }

        private void Deselect(CharacterControl commander)
        {
            _currentCommander.TargetingCommanded -= SetTarget;
            _currentCommander.SelectionCanceled -= Deselect;
            _currentCommander.AbilityUseTried -= _abilityCast.OnAbilityUseTried;
            _currentCommander = null;
            Deselected?.Invoke();
        }
    }
}