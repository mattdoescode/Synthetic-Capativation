using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject ALifePrefab;

    private float nextSpawnTime;

    public float spawnDelay = 10;

    public bool friendly = false;


    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn())
        {
            Spawn();
        }
        else
            return;
    }

    private void Spawn()
    {
        //Debug.Log("SPAWNING");
        nextSpawnTime = Time.time + spawnDelay;
        //can't instntiate with parameters so we do something else...

        //instanticate around the nest, and set nest to be the parent
        GameObject spawned = Instantiate(ALifePrefab, transform.position + new Vector3(Random.Range(-8,8), 0, Random.Range(-8, 8)), transform.rotation, gameObject.transform);
        //spawned.transform.SetParent(gameObject.transform);

        ALifeClass thisAlife = spawned.GetComponent<ALifeClass>();

        thisAlife.birthNest = gameObject;

        if (friendly)
        {
            thisAlife.lifeColor = new Color(0, 0.8f, 0, 1f);
            thisAlife.changeColor(thisAlife.lifeColor);
            thisAlife.setFriendly(true);
            thisAlife.tag = "ALife1";
        }
        else
        {
            thisAlife.lifeColor = new Color(0.8f, 0, 0, 1f);
            thisAlife.changeColor(thisAlife.lifeColor);
            thisAlife.setFriendly(false);
            thisAlife.tag = "ALife2";
        }
    }
    private bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }

    public void setFriendly(bool toBe){
        friendly = toBe;
    }
}