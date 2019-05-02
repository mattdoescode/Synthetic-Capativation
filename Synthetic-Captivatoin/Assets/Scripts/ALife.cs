using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALife : ALifeClass
{ 
    //how quickly the Alife moves
    public float speed = 1.0f;


    void Update()
    {
        string need = live();

        target = findCloseNeededResource(need);

        //need to fix target closeness
        float distanceTo = Vector3.Distance(target.transform.position, transform.position);

        if (distanceTo > 1 && target.tag != "Nest")
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else if(distanceTo > 8)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        //move this to on trigger
        else
        {
           //nothing to do here
           //code moved to on collision
        }
    }
}
