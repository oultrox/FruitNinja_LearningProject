using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

    [SerializeField] private float minDelay = .1f;
    [SerializeField] private float maxDelay = 1f;
    [SerializeField] private GameObject[] fruitsPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    private GameObject fruit;

    private float delay;
    private int spawnIndex;
    private int fruitIndex;
    private Transform spawnPoint;
    private Transform dynamicParent;

    private void Start()
    {
        StartCoroutine(SpawnFruits());
        dynamicParent = GameObject.FindGameObjectWithTag("DynamicObject").transform;
    }

    
    IEnumerator SpawnFruits () {
        while (true)
        {
            delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            spawnIndex = Random.Range(0, spawnPoints.Length);
            spawnPoint = spawnPoints[spawnIndex];

            fruitIndex = Random.Range(0, fruitsPrefabs.Length);
            fruit = Instantiate(fruitsPrefabs[fruitIndex],spawnPoint.position,spawnPoint.rotation);
            fruit.transform.SetParent(dynamicParent);
            Destroy(fruit, 3f);
        }
	}
}
