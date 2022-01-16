using Sources.Runtime.Models.Characters;
using UnityEngine;

namespace Sources.Runtime.Presenters
{
    public class EnemyPresentersFactory : PresentersFactory<Character>
    {
        [SerializeField] private CharacterPresenter _testEnemyPrefab;
        
        protected override Presenter<Character> GetPrefab(Character model)
        {
            return _testEnemyPrefab;
        }
    }
}