using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//When alife feed off a resource deplete a small amount of it.

public class Resource_Managment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("grow", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void grow()
    {
        if(checkSize())
            transform.localScale += new Vector3(0.25f, 0.05f, 0.25f);
    }

    bool checkSize()
    {
        Vector3 size = GetComponent<Collider>().bounds.size;
        if (size.x <= 2)
        {
            Destroy(gameObject);
            return false;
        } else if(size.x >= 4.5)
        {
            return true;
        }
        Debug.Log("THIS SHOULD NEVER BE REACHED");
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.localScale -= new Vector3(0.25f, 0.05f, 0.25f);
        checkSize();
    }
}
