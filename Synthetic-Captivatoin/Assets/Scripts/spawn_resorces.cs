using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_resorces : MonoBehaviour
{


    public GameObject NestPrefab;
    public GameObject waterPrefeb;
    public GameObject foodPrefab;


    int preX = 0;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("setupTerrain", 2);
    }

    void setupTerrain(){
        //spawn in everything initially

        //Setup nests
        for(int i=0; i < 5; i++){
            
        }

        //setup food + water
        for(int i=0; i < 5; i++){
            
        }


        Invoke("spawn", 5);
    }



    void spawn(){        
        Debug.Log("Running");

        //



        int x = Random.Range(3,14) + (preX / 2);
        Invoke("spawn", x);
    }

}
