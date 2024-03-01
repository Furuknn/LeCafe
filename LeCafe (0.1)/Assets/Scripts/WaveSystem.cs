using UnityEngine;
using System.Collections;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]private GameObject customerPrefab;
    [SerializeField]private Transform[] spawnPoints;

    [SerializeField]private float minSpawnInterval = 5f; 
    [SerializeField]private float maxSpawnInterval = 10f; 

    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    IEnumerator SpawnCustomers()
    {
        while (true)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(customerPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}