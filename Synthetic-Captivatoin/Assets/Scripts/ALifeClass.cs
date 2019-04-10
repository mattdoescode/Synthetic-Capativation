using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALifeClass : MonoBehaviour
{ 
    public GameObject ALifePrefab;
    public GameObject birthNest;
    public GameObject target;

    //TRAILS OF LIFE

    //Potential traits to add to the ALIFE
    //are these human traits?
    //power, independence, curiosity, acceptance, order, saving, honor, idealism, social contact, family, status, vengeance, physical exercise, and tranquility.

    //neccessaries for life
    public float hunger;
    public float thirst;
    public float rest;
    public float health;

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
 
        hunger = 100;
        thirst = 100;
        rest = 100;
        health = Random.Range(50,100);
    }

    //finite state machine here
    //this doesn't seem to be done right to me
    public string live()
    {
        //how to we determine needs? 
        //add in lazyness trait
        if (hunger < 70)
        {
            return "Food";
        }
        else if (thirst < 70)
        {
            //go get food
            return "Water";
        }
        else if (rest < 70)
        {
            //go get food
            return "Rest";
        }
        return "Nest";
    }

    public void lifeCosts()
    {
        //evertually add in code to factor in what state the ALife is in
        //i.e. if moving hunger  & thirst depletes faster
        hunger -= 2;
        rest -= 2;
        thirst -= 3;

        int totalDamage, foodDamage, thirstDamage, restDamage = 0;

        foodDamage = calculateDamages(hunger);
        thirstDamage = calculateDamages(thirst);
        restDamage = calculateDamages(rest);

        totalDamage = foodDamage + thirstDamage + restDamage;

        if(totalDamage >= 1)
        {
            if (health >= 100)
                return;
            else if(health + totalDamage >= 100)
            {
                health = 100;
                return;
            }
            else
            {
                health += totalDamage;
                return;
            }
        }
        health += totalDamage; 
        //Debug.Log(health);
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
        //Debug.Log("searching for... " + toFind);
        if (toFind == "Nest")
            return birthNest;

        GameObject[] foundItem;

        if (!friendly)
        {
            if (toFind == "Food")
            {
                foundItem = GameObject.FindGameObjectsWithTag("ALife1");
            }
            else
            {
                foundItem = GameObject.FindGameObjectsWithTag(toFind);
            }
        }
        else
        {
            foundItem = GameObject.FindGameObjectsWithTag(toFind);
        }
        if(foundItem.Length == 0)
        {
            //if we didn't find anything we return an empty game object
            //this is not the correct thing to do here
            changeColor(new Color(0, 0, 200));
            return null;
        }else if(foundItem.Length == 1)
        {
            return foundItem[0];
        }
        //determine what is closest of the needed resource
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


    //REPRODUCTOIN 
    //PRE: Method of reproduction and mate(s)
    //POST: Created Swapn
    //Factors on reproduction
    // Thirst, hunger, rest
    // reproduction takes time -> random at this point in time
    //Function to produce offsping
    //sexual and asexual means here
    //https://www.diffen.com/difference/Asexual_Reproduction_vs_Sexual_Reproduction
    public GameObject reproduce(GameObject partner = null)
    {

        Debug.Log("repo time");

        if(partner == null)
        {
            //asexual stuff here
            GameObject spawn = Instantiate(ALifePrefab, gameObject.transform.position + new Vector3(Random.Range(-0.5f,0.5f), 0, Random.Range(-0.5f, 0.5f)), gameObject.transform.rotation);
            ALifeClass thisAlife = spawn.GetComponent<ALifeClass>();
            thisAlife.changeColor(new Color(0, 1.0f, 0, 1.0f));
            thisAlife.setFriendly(true);
            thisAlife.tag = "ALife1";
        }
        else
        {
            //sexual stuff here 
        }
        return new GameObject();
    }

    int calculateDamages(float toCheck)
    {
        if (toCheck >= 60)
        {
            return 1;
        }
        else if (toCheck >= 35)
        {
            return 0;
        }
        else if (toCheck >= 25)
        {
            return -1;
        }
        else if (toCheck >= 10)
        {
            return -2;
        }
        else if (toCheck >= 5)
        {
            return -3;
        }
        else
        {
            return -5;
        }
    }

    //collision detection 
    //collisions happen with other alife and resources
    private void OnCollisionEnter(Collision collision)
    {

        string collisionObj = collision.gameObject.tag;

        switch (collisionObj)
        {
            case "Nest":
                rest = 100;
                break;


            case "Food":
                reproduce();
                if (!friendly)
                    Destroy(target);
                hunger = 100;
                break;
            //floor collisions happen a lot
            //how can this be cut down?
            case "Water":
                thirst = 100;
                break;

            case "Floor":
                break;

            case "ALife1":
                break;

            default:
                //Debug.Log("NOT anticipated collision - collison with " + collisionObj);
                break;
        }
    }
}
