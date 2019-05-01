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
        //Debug.Log(size.x);
        if (size.x <= 2)
        {
            Destroy(gameObject); 
        }
        else if (size.x <= 22)
        {
            return true;
        }
        return false;
    }

    public void setSize(Vector3 size){
        if(size.x > 22 || size.z > 22){
            size.x = 22;
            size.z = 22;
            size.y = 5;
        }
        transform.localScale = size;
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.localScale -= new Vector3(0.25f, 0.05f, 0.25f);
        checkSize();
    }
}