using System;
using Sources.External.QuickOutline.Scripts;
using Sources.Runtime.Models.CharactersStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.Characters
{
    public class CommandableCharacter : CharacterDecorator
    {
        public Action Selected;
        public Action Deselected;

        private CharacterControl _currentCommander;

        public CommandableCharacter(Character baseCharacter, CharacterBank characterBank, Outline outline) 
            : base(baseCharacter, characterBank)
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

        public override void Die()
        {
            if (!(_currentCommander is null))
                Deselect(_currentCommander);
            _baseCharacter.Die();
        }

        private void Deselect(CharacterControl commander)
        {
            _currentCommander.Commanded -= SetTarget;
            _currentCommander.SelectionCanceled -= Deselect;
            _currentCommander = null;
            Deselected?.Invoke();
        }
    }
}