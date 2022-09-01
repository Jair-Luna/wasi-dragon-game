using System.Collections;
using UnityEngine;

namespace AmusemenMiniGame
{
    public class FruitSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject fruitPrefab;
        [SerializeField] private GameObject sliceSomeWatermelonPrefab;
        [SerializeField] private Transform[] spawnPoints;

        private AmusemenMiniGame amusemenMiniGame;
        private const int PlayToSpawn = 10;

        private void Start()
        {
            StartCoroutine(StartFruitSpawner());
            amusemenMiniGame = GetComponentInParent<AmusemenMiniGame>();
        }

        private IEnumerator StartFruitSpawner()
        {
            const float minDelay = 0.3f;
            const float maxDelay = 1f;
            const float startDelay = 0.5f;

            yield return new WaitForSeconds(startDelay);

            var sliceSomeWatermelon = Instantiate(sliceSomeWatermelonPrefab, transform.position, Quaternion.identity);
            Destroy(sliceSomeWatermelon, 2f);

            yield return new WaitForSeconds(startDelay * 2);

            for (var i = 0; i < PlayToSpawn; i++)
            {
                SpawnPlay();
                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            }

            yield return new WaitForSeconds(1f);
            amusemenMiniGame.StopAmusemenMiniGame();
            Destroy(gameObject);
        }

        private void SpawnPlay()
        {
            var spawnIndex = Random.Range(0, spawnPoints.Length);
            var spawnPoint = spawnPoints[spawnIndex];

            var fruit = Instantiate(fruitPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(fruit, 5f);
        }
    }
}