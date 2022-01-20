using System.Collections;
using Sources.Runtime.Models.Characters;
using Sources.Runtime.Presenters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Runtime
{
    public class EnemyWaveSimulation : MonoBehaviour
    {
        [SerializeField] 
        private EnemyFactory _factory;
        [SerializeField] 
        private CharacterBank _characterBank;
        
        [Header("Wave Configs")]
        [SerializeField] 
        private Vector2 _waveAreaSize;
        [SerializeField] 
        private int _enemyCount;
        [SerializeField] 
        private int _waveTestInterval = 5;

        [Header("Test Enemy Configs")] 
        [SerializeField] 
        private int _healthValue = 1;
        [SerializeField] 
        private float _minAttackDistance = 2;
        [SerializeField] 
        private float _maxAttackDistance = 2;

        private void Start()
        {
            StartCoroutine(TestSpawnWave());
        }

        private IEnumerator TestSpawnWave()//TODO: Separate wave in other object
        {
            while (true)
            {
                SpawnWave();
                yield return new WaitForSeconds(_waveTestInterval);
            }
        }

        private void SpawnWave()
        {
            for (var i = 0; i < _enemyCount; i++)
            {
                Vector3 position = GetRandomAreaPosition();
                var model = new Enemy(position, Quaternion.identity, _healthValue, _characterBank,
                    _minAttackDistance, _maxAttackDistance);
                _factory.Create(model);
            }
        }

        private Vector3 GetRandomAreaPosition()
        {
            var result = Random.Range(0, 2) == 0
                ? new Vector3(Random.Range(-_waveAreaSize.x / 2, _waveAreaSize.x / 2),0)
                : new Vector3(0,0, Random.Range(-_waveAreaSize.y / 2, _waveAreaSize.y / 2));
            if(Random.Range(0, 2) == 0)
                result.x = Random.Range(0, 2) == 0 ? -_waveAreaSize.x / 2 : _waveAreaSize.x / 2;
            else
                result.z = Random.Range(0, 2) == 0 ? -_waveAreaSize.y / 2 : _waveAreaSize.y / 2;
            result += transform.position;
        
            return result;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(255, 0,0, .5f);
            var gizmoSize = new Vector3(_waveAreaSize.x, 0, _waveAreaSize.y);
            Gizmos.DrawWireCube(transform.position, gizmoSize);
        }
    }
}
