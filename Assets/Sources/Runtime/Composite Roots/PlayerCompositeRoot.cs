using Sources.Runtime.Input;
using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Composite_Roots
{
    public class PlayerCompositeRoot : MonoBehaviour
    {
        private PlayerPresenter _presenter;
        private InputRouter _inputRouter;
    
        private void Awake()
        {
            _presenter = GetComponent<PlayerPresenter>();
            _presenter.Init(new Player(transform.position, transform.rotation));
            _inputRouter = new InputRouter(new CharacterControl());
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
