using Sources.Runtime.Models;
using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public class EnemyFactory : PresentersFactory<Character>
    {
        [SerializeField] private CharacterPresenter _testEnemyPrefab;
        
        protected override Presenter<Character> GetPrefab(Character model)
        {
            var weapon = new MeleeWeapon(1, 2, 3,
                _testEnemyPrefab.GetComponentInChildren<DamageDealerPresenter>().Model);
            model.BindWeapon(weapon);
            return _testEnemyPrefab;
        }
    }
}