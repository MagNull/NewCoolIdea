using Sources.Runtime.Input;
using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Composite_Roots
{
    public class PlayerCompositeRoot : MonoBehaviour
    {
        [SerializeField] private CharacterPresenter _warriorPresenter;
        [SerializeField] private CharacterPresenter _enchanterPresenter;
        [SerializeField] private CharacterPresenter _rangerPresenter;
        private PlayerPresenter _presenter;
        private InputRouter _inputRouter;
    
        private void Awake()
        {
            _presenter = GetComponent<PlayerPresenter>();
            _presenter.Init(new Player(transform.position, transform.rotation));
            _inputRouter = new InputRouter(new CharacterControl(),
                (CommandableCharacter) _warriorPresenter.Model,
                (CommandableCharacter) _enchanterPresenter.Model,
                (CommandableCharacter) _rangerPresenter.Model);
        }

        private void OnEnable()
        {
            _inputRouter.OnEnable();
        }

        private void OnDisable()
        {
            _inputRouter.OnDisable();
        }
    }
}
