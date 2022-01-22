using Sources.Runtime.Models;
using Sources.Runtime.Presenters;
using UnityEngine;

namespace Sources.Runtime.Composite_Roots
{
    [RequireComponent(typeof(DamageDealerPresenter))]
    public class DamageDealerInitializer : MonoBehaviour
    {
        [SerializeField] 
        private int _damage = 1;
        private DamageDealerPresenter _presenter;
        private DamageDealer _model;
        
        private void Awake()
        {
            _presenter = GetComponent<DamageDealerPresenter>();
            _model = new DamageDealer(transform.position, transform.rotation, _damage);
            _presenter.Init(_model);
        }
    }
}