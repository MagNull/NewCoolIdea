using Sources.Runtime.Models;

namespace Sources.Runtime.Input
{
    public class InputRouter
    {
        private PlayerInput _input;
        private CharacterControl _characterControl;
        
        public InputRouter(Player player, CharacterControl characterControl)
        {
            _characterControl = characterControl;
            _input = new PlayerInput();
        }

        public void OnEnable()
        {
            _input.Enable();
            _input.Player.SelectCharacter.performed += _ => _characterControl.SelectCharacter();
            _input.Player.TargetCommand.performed += _ => _characterControl.Command();
            _input.Player.CancelSelection.performed += _ => _characterControl.CancelSelection();
        }

        public void OnDisable()
        {
            _input.Disable();
        }
    }
}