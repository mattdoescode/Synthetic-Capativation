using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALifeClass : MonoBehaviour
{ 
    public GameObject ALifePrefab;

    //TRAILS OF LIFE

    //Potential traits to add to the ALIFE
    //are these human traits?
    //power, independence, curiosity, acceptance, order, saving, honor, idealism, social contact, family, status, vengeance, physical exercise, and tranquility.

    //neccessaries for life
    public float hunger;
    public float thirst;
    public float rest;

    //reproduction drive
    public float romance;

    //curosity -> exploring more
    public float curosity;

    public bool friendly;

    public void Start()
    {
        //stops all rotation through physics simulations
        GetComponent<Rigidbody>().freezeRotation = true;

        //run lives costs every 2 seconds
        InvokeRepeating("lifeCosts", 5, 2);

        //make this dynamic 
        hunger = 50;
        thirst = 50;
        rest = 100;
    }

    //finite state machine here
    //this doesn't seem to be done right to me
    public string live()
    {
        //there has to be a better way of doing this...
        if (friendly)
        {
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
        }
        else
        {
            return "ALife1";
        }

        return "Nest";
    }

    public void lifeCosts()
    {
        //evertually add in code to factor in what state the ALife is in
        //i.e. if moving hunger  & thirst depletes faster
        hunger--;
        rest--;
        thirst--;
        //Debug.Log(hunger + " " + thirst + " " + rest);
    }

    //REPRODUCTOIN 
    //PRE: Method of reproduction and mate(s)
    //POST: Created Swapn

    //Factors on reproduction
    // Thirst, hunger, rest
    // reproduction takes time -> random at this point in time

    public GameObject reproduce(int method, GameObject parent1 = null, GameObject parent2 = null)
    {
        return new GameObject();
    }

    //Spawning life randomly at the start of the scene
    //Random methods will not be used for long 
    //PRE: Local land conditions & enviromental factors
    //POST: simpliest form of life
    public void Instantiate(int xPos, int yPos)
    {
        //set everything here
        Debug.Log("xPos");
        //return Instantiate(ALifePrefab);
    }

    //change the current Alifes color
    public void changeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color ;
    }

    public void setFriendly(bool toSet)
    {
        friendly = toSet;
    }

    //PRE: String of Tag for a gameobject to find 
    //POST: Returning game object with matching tag that is closest to play
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

    private void OnCollisionEnter(Collision collision)
    {

        string collisionObj = collision.gameObject.tag;

        switch (collisionObj)
        {
            case "Food":
                hunger = 100;
                break;
            //floor collisions happen a lot
            //how can this be cut down?
            case "Water":
                thirst = 100;
                break;

            case "Floor":
                break;
            
            default:
                //Debug.Log("NOT anticipated collision - collison with " + collisionObj);
                break;
        }
    }
}
