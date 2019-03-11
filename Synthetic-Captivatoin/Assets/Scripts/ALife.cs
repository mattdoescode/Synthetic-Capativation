using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALife : ALifeClass
{ 
    public float speed = 1.0f;

    GameObject[] foodItem;
    GameObject Selection;

    void Update()
    {
        GameObject test = findCloseNeededResource("Food");

        if (Vector3.Distance(test.transform.position,transform.position) > 13)
        {
            transform.position = Vector3.MoveTowards(transform.position, test.transform.position, speed * Time.deltaTime);
        }
    }
}
