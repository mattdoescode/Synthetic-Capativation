using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject ALifePrefab;

    private float nextSpawnTime;

    public float spawnDelay = 10;

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn())
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        nextSpawnTime = Time.time + spawnDelay;
        //can't instntiate with parameters so we do something else...
        GameObject spawned = Instantiate(ALifePrefab, transform.position, transform.rotation);

        //GameObject newObject = Instantiate(ALifePrefab) as GameObject;
        //ALifeClass theObject = newObject.GetComponent<ALifeClass>();
        ALifeClass alc = spawned.GetComponent<ALifeClass>();

        spawned.GetComponent<Renderer>().material.color = Color.green;


        //alc.live();


        //ALifeClass spawnedScript = spawned.GetComponent<>();
    }

    private bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }
}
