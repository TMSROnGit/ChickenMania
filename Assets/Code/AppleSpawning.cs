using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawning : MonoBehaviour
{

    public GameObject[] applePrefab;
    public float spawnRate = 5f;
    public float groundHeight = 0f;
    private GameObject currentApple;

    // Start is called before the first frame update
    void Start()
    {
       SpawnApple();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnAppleTimer()
    {
        // Calls the SpawnApple method every 5 seconds
        while (true)
        {
            SpawnApple();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnApple() 
    {
        // Spawns the apple at a random position from the applePrefab array inside the defined range and stores the current apple
        int randomIndex = Random.Range(0, applePrefab.Length);
        Vector3 randomPosition = new Vector3(Random.Range(-17, 17), groundHeight, Random.Range(-10, 10));
        currentApple = Instantiate(applePrefab[randomIndex], randomPosition, Quaternion.identity);
    }

    public void catchedApple()
    {
        // Destroys the catched apple and calls the spawn apple to spawn a new one at a random location
        Destroy(currentApple);
        SpawnApple();
    }
}
