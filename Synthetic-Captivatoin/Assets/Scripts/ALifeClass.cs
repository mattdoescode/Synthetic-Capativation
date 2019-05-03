using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALifeClass : MonoBehaviour
{ 
    public GameObject ALifePrefab;
    public GameObject birthNest;
    public GameObject target;

    public Color lifeColor;

    public bool hasNeedNotFilled = false;  

    //how fast the alive move

    //TRAILS OF LIFE

    //Potential traits to add to the ALIFE
    //are these human traits?
    //power, independence, curiosity, acceptance, order, saving, honor, idealism, social contact, family, status, vengeance, physical exercise, and tranquility.

    //neccessaries for life
    public float hunger;
    public float thirst;
    public float rest;
    public float health;
    public float movementSpeed;
    //reproduction drive
    //public float romance;

    //curosity -> exploring more
    //public float curosity;

    public bool friendly;

    public void Start()
    {
        //stops all rotation through physics simulations
        GetComponent<Rigidbody>().freezeRotation = true;

        //run lives costs every 2 seconds
        InvokeRepeating("lifeCosts", 5, 2);

        //if the alife fell off of the map kill it
        InvokeRepeating("falling", 30, 30);

        //reproduction time
        //reproduction time


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
            return "Water";
        }
        else if (rest < 70)
        {
            return "Nest";
        }
        return "Nest";
    }

    public void lifeCosts()
    {
        if(health < 0)
            Destroy(gameObject);

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
        /// https://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color/801431
        /// Creates color with corrected brightness
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            //Debug.Log(color);

            //float red,green,blue;

            if(gameObject.tag == "ALife2")
            {
                color.r = color.r / 2; 
                return (new Color(color.r, lifeColor.g, lifeColor.b));
            }
            if(gameObject.tag == "ALife1")
            {
                color.g = color.g / 2; 
                return (new Color(lifeColor.r, color.g, lifeColor.b));
            }
            
            return lifeColor;

            //Debug.Log(new Color(red, green, blue));
            //return new Color(red, green, blue);


            // I CAN'T GET THE COLORS WORKING....
            // DOes this have something to do with FLOAT math? 
            // if(color.r > 0)
            //      red = (float)color.r;
            // else
            //      red = 0f;

            // if(color.g > 0)
            //      green = (float)color.g;
            // else
            //      green = 0f;

            // Debug.Log(green);

            // if(color.b > 0)
            //      blue = (float)color.b;
            // else
            //      blue = 0f;

            // if (correctionFactor > 0)
            // {
            //     correctionFactor = 1 + correctionFactor;
            //     red *= correctionFactor;
            //     green *= correctionFactor;
            //     blue *= correctionFactor;
            // }
            // else
            // {
            //     red = color.r - correctionFactor;
            //     blue = color.b - correctionFactor;
            //     green = color.g - correctionFactor;
            //     Debug.Log(green);
            // }

            // Debug.Log(new Color(red, green, blue));
            // return new Color(red, green, blue);
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

            if(!hasNeedNotFilled){
                lifeColor = ChangeColorBrightness(lifeColor, -0.3f);
                changeColor(lifeColor);
                hasNeedNotFilled = true;
            }

            return birthNest;
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
        changeColor(lifeColor);
        if (!friendly && toFind == "Food")
            target = foundItem[cloestObjectKnown];
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
    public void reproduce()
    {

        Debug.Log("repo time");
    
            //asexual stuff here
            GameObject spawn = Instantiate(ALifePrefab, gameObject.transform.position + new Vector3(Random.Range(-0.5f,0.5f), 0, Random.Range(-0.5f, 0.5f)), gameObject.transform.rotation);
            ALifeClass thisAlife = spawn.GetComponent<ALifeClass>();

            if(gameObject.tag == "ALife1"){
                thisAlife.friendly = true;
                thisAlife.tag = "ALife1";
                thisAlife.lifeColor = new Color(0, 0.8f, 0, 1f);
                thisAlife.movementSpeed = Random.Range(18,25);

            } else {
                thisAlife.friendly = false;
                thisAlife.tag = "ALife2";
                thisAlife.lifeColor = new Color(0.8f, 0, 0, 1f);
                thisAlife.movementSpeed = Random.Range(16,23);

            }
            thisAlife.changeColor(thisAlife.lifeColor);
            thisAlife.birthNest = birthNest;

            thisAlife.movementSpeed = movementSpeed + Random.Range(-2,2);
            float scale = Random.Range(-0.1f,0.01f);
            thisAlife.transform.localScale = gameObject.transform.localScale + new Vector3(scale, scale, scale);
            

            
        
        return;
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

    //destory game object if it fell
    private void falling()
    {
        if (gameObject.transform.position.y < -20)
            Destroy(gameObject);
    }
 
    //collision detection 
    //collisions happen with other alife and resources
    private void OnCollisionEnter(Collision collision)
    {
        string collisionObj = collision.gameObject.tag;

        //Debug.Log("COLLISION WITH " + collisionObj);

        switch (collisionObj)
        {
            case "Nest":
                rest = 100;
                break;
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
            case "ALife1":
                
                if(gameObject.tag == "ALife2" && target != null && target.tag == "ALife1")
                {
                    Destroy(target);
                    hunger = 100;
                }
                break;
            default:
                //if(gameObject.tag != null && target != null)
                   //Debug.Log("NOT anticipated collision - " + gameObject.tag + " collided with " + collisionObj + ". Target was " + target.tag);
                break;
        }
    }
}