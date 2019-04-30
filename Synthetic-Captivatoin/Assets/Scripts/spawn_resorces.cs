using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_resorces : MonoBehaviour
{

    int preX = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawn", 2);
    }

    void spawn(){

        int x = Random.Range(3,14) + (preX / 2);
        



        Invoke("spawn", x);
    }

}
