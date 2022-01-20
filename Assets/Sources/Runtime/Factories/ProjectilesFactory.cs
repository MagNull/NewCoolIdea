using Sources.Runtime.Models;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public class ProjectilesFactory : PresentersFactory<DamageDealer>
    {
        [SerializeField] private DamageDealerPresenter _testProjectilePrefab;
        
        protected override Presenter<DamageDealer> GetPrefab(DamageDealer model)
        {
            return _testProjectilePrefab;
        }
    }
}