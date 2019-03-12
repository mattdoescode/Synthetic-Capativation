using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALifeClass : MonoBehaviour
{
    //traits of the ALife
    public float hunger;
    public float thirst;
    public float rest;
    //enum LifeFactors
    //{

    //}

    //current state of the Alife (what is it doing)
    enum CurrentState {
        eatting = 0,
        drinking = 0,
        resting = 0
    };



    public void Start()
    {
        //stops all rotation through physics simulations
        GetComponent<Rigidbody>().freezeRotation = true;
        //run lives costs every 2 seconds
        InvokeRepeating("lifeCosts", 5, 1);

        hunger = 50;
        thirst = 50;
        rest = 100;
    }

    //finite state machine here
    //this doesn't seem to be done right to me
    public string live()
    {
        //there has to be a better way of doing this...
        if (hunger < 90)
        {
            //go get water
            return "Food";
        }
        else if (thirst < 80)
        {
            //go get food
            return "Water";
        }
        else if (hunger < 80)
        {
            //go get food
            return "Rest";
        }

        return "Nest";
    }


    public void lifeCosts()
    {
        hunger--;
        rest--;
        thirst--;
        Debug.Log(hunger + " " + thirst + " " + rest);
    }


    //PRE: String of Tag for a gameobject to find 
    //POST: Returning game object with matching tag that is closest to play
    public GameObject findCloseNeededResource(string toFind)
    {
        //Debug.Log("searching for... " + toFind);
        GameObject[] foundItem = GameObject.FindGameObjectsWithTag(toFind);
        if(foundItem.Length == 0)
        {
            //if we didn't find anything we return an empty game object
            //i think this is really wrong to do
            return new GameObject();
        }else if(foundItem.Length == 1)
        {
            return foundItem[0];
        }

        //sort through multiple found objects
        //select cloest item
        int cloestObjectKnown = 0;
        for(int i = 0; i < foundItem.Length; i++)
        {
            if(Vector3.Distance(foundItem[i].transform.position,transform.position) <
               Vector3.Distance(foundItem[cloestObjectKnown].transform.position, transform.position))
            {
                cloestObjectKnown = i;
            }
        }
        return foundItem[cloestObjectKnown];
    }
}
