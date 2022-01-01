using System;
using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Models
{
    public class CharacterControl
    {
        public Action<object> Commanded;
        public Action<CharacterControl> SelectionCanceled;
        private readonly Camera _camera;

        public CharacterControl()
        {
            _camera = Camera.main;
        }
        
        public void SelectCharacter()
        {
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out CharacterPresenter presenter))
                {
                    if (presenter.Model is CommandableCharacter commandableCharacter)
                    {
                        commandableCharacter.Select(this);
                    }
                }
                else
                {
                    CancelSelection();
                }
            }
        }

        public void Command()
        {
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out CharacterPresenter presenter))
                {
                    Debug.Log("Character");
                    Commanded?.Invoke(presenter.Model);
                }
                else
                {
                    Commanded?.Invoke(hit.point);
                }
            }
        }

        public void CancelSelection()
        {
            SelectionCanceled?.Invoke(this);
        }
    }
}