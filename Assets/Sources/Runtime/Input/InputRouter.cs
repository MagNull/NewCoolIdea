using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;

namespace Sources.Runtime.Input
{
    public class InputRouter
    {
        private readonly PlayerInput _input;
        private readonly CharacterControl _characterControl;
        private readonly CommandableCharacter _warrior;
        private readonly CommandableCharacter _enchanter;
        private readonly CommandableCharacter _ranger;
        
        public InputRouter(CharacterControl characterControl, 
            CommandableCharacter warrior, CommandableCharacter enchanter, CommandableCharacter ranger)
        {
            _characterControl = characterControl;
            _warrior = warrior;
            _enchanter = enchanter;
            _ranger = ranger;
            _input = new PlayerInput();
        }

        public void OnEnable()
        {
            _input.Enable();
            _input.Player.SelectCharacter.performed += _ => _characterControl.SelectCharacter();
            _input.Player.TargetCommand.performed += _ => _characterControl.Command();
            _input.Player.CancelSelection.performed += _ => _characterControl.CancelSelection();
            _input.Player.WarriorHotkey.performed += _ =>
            {
                _characterControl.CancelSelection();
                _warrior.Select(_characterControl);
            };
            _input.Player.EnchanterHotkey.performed += _ =>
            {
                _characterControl.CancelSelection();
                _enchanter.Select(_characterControl);
            };
            _input.Player.RangerHotkey.performed += _ =>
            {
                _characterControl.CancelSelection();
                _ranger.Select(_characterControl);
            };
        }

        public void OnDisable()
        {
            _input.Disable();
        }
    }
}