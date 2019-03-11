using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALife : MonoBehaviour
{

    public float speed = 1.0f;

    GameObject[] foodItem;
    GameObject Selection;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello world");

        foodItem = GameObject.FindGameObjectsWithTag("Food");
        Selection = foodItem[0];

        Debug.Log(Selection.transform.position);

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Selection.transform.position, speed * Time.deltaTime);
   
    }
}
