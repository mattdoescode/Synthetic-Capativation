using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALifeAttack : ALifeClass
{
    //how quickly the Alife moves
    public float speed = 5.0f;

    bool hunger = true;
    bool justAte = false;


    void Update()
    {
        string need = live();

        GameObject test = findCloseNeededResource("Life");

        if (Vector3.Distance(test.transform.position, transform.position) > 3 && hunger)
        {
            transform.position = Vector3.MoveTowards(transform.position, test.transform.position, speed * Time.deltaTime);
            justAte = true;
        }
        else if(justAte)
        {
            Destroy(test);
            hunger = false;
            GoHome();
        }
    }

    void GoHome()
    {
        GameObject test = findCloseNeededResource("hunterNest");
        transform.position = Vector3.MoveTowards(transform.position, test.transform.position, speed * Time.deltaTime);

        if(Vector3.Distance(test.transform.position, transform.position) > 3){
            hunger = true;
        }
    }
}
