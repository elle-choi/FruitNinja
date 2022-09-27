using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;

    public GameObject bombPrefab; // separate variable bc bomb should have a smaller chance of spawning
    [Range(0f, 1f)] // bombChance becomes between a range (slider in Unity)
    public float bombChance = 0.05f; // 5% chance of a bomb spawning instead of a fruit

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;


    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable() // called when script is enabled
    {
        Debug.Log("Spawner Enabled");
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        Debug.Log("Spawner Disabled");
        StopAllCoroutines();
    }

    
    private IEnumerator Spawn()
    {
        Debug.Log("Spawner called");
        Time.timeScale = 1f;
        // initial delay so the spawn doesn't happen immediately
        // hard coded 2 seconds
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            // picking a random fruit
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];


            // instantiating the bomb rather than the fruit
            if (Random.value < bombChance)
            {
                prefab = bombPrefab;
            }


            Vector3 position = new Vector3();

            // picking a random x, y, z position within bounds of collider we defined
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // Quaternion = Rotation
            // x and y stay the same, z is a random angle 
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));


            // returns the new object (fruit)
            GameObject fruit = Instantiate(prefab, position, rotation);
            // fruit destroyed after max life time
            Destroy(fruit, maxLifetime);


            // adding force to fruit
            float force = Random.Range(minForce, maxForce);
            // not sure what this line means
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

        }
    }


}
