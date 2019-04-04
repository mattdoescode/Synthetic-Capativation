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

        GameObject test = findCloseNeededResource(need);

        if (Vector3.Distance(test.transform.position,transform.position) > 13)
        {
            transform.position = Vector3.MoveTowards(transform.position, test.transform.position, speed * Time.deltaTime);
        } else
        {
            switch (need)
            {
                case "Nest":
                    rest = 100;
                    break;
                case "Food":
                    hunger = 100;
                    break;
                case "Water":
                    thirst = 100;
                    break;
                default:
                    break;
            }
        }
    }
}
