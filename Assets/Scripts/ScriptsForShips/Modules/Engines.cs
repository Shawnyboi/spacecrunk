using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : Module
{

    override protected IEnumerator Fire()
    {
        //Allow for ship movement for a time
        yield return null;
    }

}
