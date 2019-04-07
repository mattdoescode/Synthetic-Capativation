using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//When alife feed off a resource deplete a small amount of it.

public class Resource_Managment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.localScale -= new Vector3(0.25f, 0.05f, 0.25f);
        Vector3 size = GetComponent<Collider>().bounds.size;

        if(size.x < 2)
        {
            Destroy(gameObject);
        }
    }
}
