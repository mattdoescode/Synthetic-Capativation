using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newnew : ALifeClass
{
    //how quickly the Alife moves
    public float speed = 1.0f;

    void Update()
    {
        string need = live();
    }
}
