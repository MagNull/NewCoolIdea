using System;
using Sources.External.QuickOutline.Scripts;

namespace Sources.Runtime.Models.Characters
{
    public class CommandableCharacter : CharacterDecorator
    {
        public event Action Selected;
        public event Action Deselected;

        private CharacterControl _currentCommander;

        public CommandableCharacter(Character baseCharacter, CharacterBank characterBank, Outline outline) 
            : base(baseCharacter, characterBank)
        {
            Selected += () => outline.enabled = true;
            Deselected += () => outline.enabled = false;
            _baseCharacter.Health.Died += () =>
            {
                if (!(_currentCommander is null))
                    Deselect(_currentCommander);
            };
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
    }
}