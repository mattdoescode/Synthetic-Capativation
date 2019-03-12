using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALifeClass : MonoBehaviour
{
    //traits of the ALife
    public float hunger;
    public float thirst;

    public void Start()
    {
        //stops all rotation through physics simulations
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    //finite state machine here
    //this doesn't seem to be done right to me
    public void live(float h, float t)
    {
        //thirst is more important than hunger is 

    }

    public GameObject findCloseNeededResource(string toFind)
    {
        Debug.Log("searching for... " + toFind);
        GameObject[] foundItem = GameObject.FindGameObjectsWithTag(toFind);
        if(foundItem.Length == 0)
        {
            //if we didn't find anything we return an empty game object
            //i think this is really wrong to do
            return new GameObject();
        }else if(foundItem.Length == 1)
        {
            Debug.Log("found 1 food");
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
