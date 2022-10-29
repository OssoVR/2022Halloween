using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemy[] monsters;
    public float spawnRate = 2f;
    float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(monsters[Random.Range(0, monsters.Length)], this.transform.position, Quaternion.identity);
        spawnTimer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
        {
            Instantiate(monsters[Random.Range(0, monsters.Length)], this.transform.position, Quaternion.identity);
            spawnTimer = spawnRate;
        }
        else
            spawnTimer -= Time.deltaTime;

        if (spawnRate > 0.5f)
            spawnRate -= Time.deltaTime / 100f;
    }
}
