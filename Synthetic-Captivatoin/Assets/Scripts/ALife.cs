﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALife : ALifeClass
{ 
    void Update()
    {
        string need = live();

        target = findCloseNeededResource(need);

        //need to fix target closeness
        if(target != null){    
            float distanceTo = Vector3.Distance(target.transform.position, transform.position);

            if (distanceTo > 1 && target.tag != "Nest")
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            }
            else if(distanceTo > 8)
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            //move this to on trigger
            else
            {
            //nothing to do here
            //code moved to on collision
            }
        }
    }
}
