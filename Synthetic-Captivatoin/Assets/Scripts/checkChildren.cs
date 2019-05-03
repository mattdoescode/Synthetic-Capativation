using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("checkChildCount", 60, 20);
    }

    private void checkChildCount(){
        if(transform.childCount <= 0){
            Destroy(gameObject);
        } 
    }
}
