using Sources.Runtime.Models;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public class ProjectilesFactory : PresentersFactory<Projectile>
    {
        [SerializeField] private ProjectilePresenter _testProjectilePrefab;
        
        protected override Presenter<Projectile> GetPrefab(Projectile model)
        {
            return _testProjectilePrefab;
        }
    }
}