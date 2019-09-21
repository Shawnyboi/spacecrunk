using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Module
{
    override protected void Fire()
    {
        //Make a shield around the shit
    }

    public override void Turn(bool clockwise)
    {
        DefaultTurn(clockwise);
    }

}
