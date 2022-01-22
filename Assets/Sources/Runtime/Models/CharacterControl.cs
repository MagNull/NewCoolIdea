using System;
using Sources.Runtime.Input;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class CharacterControl
    {
        public event Action<dynamic> TargetingCommanded;
        public event Action<int> AbilityUseTried;
        public event Action<CharacterControl> SelectionCanceled;
        
        private readonly Camera _camera;
        
        private readonly InputRouter _inputRouter;
        private readonly CommandableCharacter _warrior;
        private readonly CommandableCharacter _enchanter;
        private readonly CommandableCharacter _ranger;

        public CharacterControl(InputRouter inputRouter, 
            CommandableCharacter warrior, CommandableCharacter enchanter, CommandableCharacter ranger)
        {
            _camera = Camera.main;
            _inputRouter = inputRouter;
            _warrior = warrior;
            _ranger = ranger;
            _enchanter = enchanter;
        }

        public void Init()
        {
            _inputRouter.Input.Player.SelectCharacter.performed += _ => SelectCharacter();
            _inputRouter.Input.Player.TargetCommand.performed += _ => TargetingCommand();
            _inputRouter.Input.Player.CancelSelection.performed += _ => CancelSelection();
            _inputRouter.Input.Player.WarriorHotkey.performed += _ =>
            {
                CancelSelection();
                _warrior.Select(this);
            };
            _inputRouter.Input.Player.EnchanterHotkey.performed += _ =>
            {
                CancelSelection();
                _enchanter.Select(this);
            };
            _inputRouter.Input.Player.RangerHotkey.performed += _ =>
            {
                CancelSelection();
                _ranger.Select(this);
            };
            _inputRouter.Input.Player.Ability1.performed += _ => AbilityUseTried?.Invoke(1);
            _inputRouter.Input.Player.Ability2.performed += _ => AbilityUseTried?.Invoke(2);
            _inputRouter.Input.Player.Ability3.performed += _ => AbilityUseTried?.Invoke(3);
        }
        
        private void SelectCharacter()
        {
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                CancelSelection();
                if (hit.collider.TryGetComponent(out CharacterPresenter presenter))
                {
                    if (presenter.Model is CommandableCharacter commandableCharacter)
                    {
                        commandableCharacter.Select(this);
                    }
                }
            }
        }

        private void TargetingCommand()
        {
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out CharacterPresenter presenter))
                {
                    TargetingCommanded?.Invoke(presenter.Model);
                }
                else
                {
                    TargetingCommanded?.Invoke(hit.point);
                }
            }
        }

        private void CancelSelection()
        {
            SelectionCanceled?.Invoke(this);
        }
    }
}