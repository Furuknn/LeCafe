using System;
using UnityEngine;
using System.Collections;
using TMPro;
using Random = UnityEngine.Random;

public class WaveSystem : MonoBehaviour
{
    #region Variables

    public GameObject coffeeCustomerPrefab;
    public GameObject dessertCustomerPrefab;
    public GameObject lasagnaCustomerPrefab;


    public TextMeshProUGUI waveData;
    public GameObject pressRText;

    [SerializeField] private Transform[] spawnPoints;

     private float minSpawnInterval = 5f;
     private float maxSpawnInterval = 10f;

    // Spawn rates for each customer type
    private float spawnRateCoffeeCustomer = 0.7f;
    private float spawnRateDessertCustomer = 0.2f;
    private float spawnRateLasagnaCustomer = 0.1f;

    // Number of customers in each wave
    private int initialWaveSize = 5;
    private int waveSizeIncrement = 2;
    private int day = 1;

    public bool isWaveOngoing = false;

    //Speeding up the game makes debugging easier.
    [Range(0,10)]
    public int timeScaleMultiplier=1;

    #endregion


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isWaveOngoing)
        {
            StartCoroutine(SpawnCustomerWaves());
            waveData.text = "Day " + day;
        }

        if (!isWaveOngoing && !pressRText.activeSelf)
        {
            pressRText.SetActive(true);
            
        }
        else if (isWaveOngoing && pressRText.activeSelf)
        {
            pressRText.SetActive(false);
        }

        Time.timeScale=timeScaleMultiplier;
    }

    IEnumerator SpawnCustomerWaves()
    {
        isWaveOngoing=true;

        int waveSize = initialWaveSize;
        int customersSpawned = 0;

        while (customersSpawned < waveSize)
        {

            // Determine a random number between 0 and 1
            float randomValue = Random.value;
            GameObject customerPrefab;

            if (randomValue < spawnRateCoffeeCustomer)
            {
                customerPrefab = coffeeCustomerPrefab;
            }
            else if (randomValue < spawnRateCoffeeCustomer + spawnRateDessertCustomer)
            {
                customerPrefab = dessertCustomerPrefab;
            }
            else
            {
                customerPrefab = lasagnaCustomerPrefab;
            }

            //Spawn the customer on a random spawnpoint.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(customerPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);

            customersSpawned++;

            //wait for a certain amount before spawning another customer.
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }

        // Increase the wave size for the next wave
        initialWaveSize += waveSizeIncrement;

        customersSpawned = 0;
        day++;

        if (day<=5)
        {
            // Adjust spawn rates for the next wave
            spawnRateCoffeeCustomer *= 0.65f; // Decrease coffee customer spawn rate by 35%
            spawnRateDessertCustomer *= 1.25f; // Increase dessert customer spawn rate by 25%
            spawnRateLasagnaCustomer *= 1.5f; // Increase lasagna customer spawn rate by 50%
            Debug.Log(spawnRateCoffeeCustomer + "    " + spawnRateDessertCustomer + "    " + spawnRateLasagnaCustomer);
        }
        

        // Clamp spawn rates to ensure they stay within valid ranges
        spawnRateCoffeeCustomer = Mathf.Clamp(spawnRateCoffeeCustomer, 0f, 1f);
        spawnRateDessertCustomer = Mathf.Clamp(spawnRateDessertCustomer, 0f, 1f);
        spawnRateLasagnaCustomer = Mathf.Clamp(spawnRateLasagnaCustomer, 0f, 1f);

        isWaveOngoing = false;
    }
}