using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_resorces : MonoBehaviour
{
    public GameObject NestPrefab;
    public GameObject waterPrefeb;
    public GameObject foodPrefab;
    public GameObject terrain;

    GameObject thisResource;

    int preX = 0;
    float xPos, zPos;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("setupTerrain", 0);
    }

    void setupTerrain(){
        //Debug.Log(terrain.GetComponent<Collider>().bounds.size.x);
        Vector3 bounds = terrain.GetComponent<Collider>().bounds.size;

       //Instantiate(waterPrefeb, new Vector3(xPos, 0, zPos), transform.rotation);

        //spawn in everything initially
        GameObject thisNest;
        //Setup nests
        for(int i=0; i < 6; i++){
            xPos = Random.Range(-bounds.x / 2,bounds.x /2);
            zPos = Random.Range(-bounds.z / 2,bounds.z /2); 
            thisNest = Instantiate(NestPrefab, new Vector3(xPos, 0.3f, zPos), transform.rotation);
            Spawner nestSpawn = thisNest.GetComponent<Spawner>();

            //PICK what type of Alife to spawn 
            //60% friendly 40% killer
            if(Random.Range(0,10) <= 6){
               //spawn friendly nest
               nestSpawn.setFriendly(true);

            } else {
                //unfriendly nest
                nestSpawn.setFriendly(false);
            }
        }

        //setup food + water
        for(int i=0; i < 45; i++){
            spawnFoodOrWater();
        }
        Invoke("spawn", 5);
    }

    void spawnFoodOrWater()
    {
        Vector3 bounds = terrain.GetComponent<Collider>().bounds.size;
        xPos = Random.Range(-bounds.x / 2,bounds.x /2);
        zPos = Random.Range(-bounds.z / 2,bounds.z /2); 

        if(Random.Range(0,10) <= 4){
            thisResource = Instantiate(foodPrefab, new Vector3(xPos, 0.3f, zPos), transform.rotation);
        } else {
            thisResource = Instantiate(waterPrefeb, new Vector3(xPos, 0.3f, zPos), transform.rotation);
        }
    }
    
    void spawn(){
        spawnFoodOrWater();
        int x = Random.Range(3,14) + (preX / 2);
        Invoke("spawn", x);
    }

}
