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

       GameObject target = findCloseNeededResource(need);
 
        if(target == null)
        {
            return;
        }

        //need to fix target closeness
        float distanceTo = Vector3.Distance(target.transform.position, transform.position);

        if (distanceTo > 1 && need != "Nest")
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else if(distanceTo > 8)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        else
        {
            switch (need)
            {
                case "Nest":
                    rest = 100;
                    break;
                case "Food":
                    if (!friendly)
                        Destroy(target);
                    hunger = 100;
                    break;
                case "Water":
                    thirst = 100;
                    break;
                case "Alife1":
                    //delete the alife in consideration
                    hunger = 100;
                    break;
                default:
                    break;
            }
        }
    }
}
