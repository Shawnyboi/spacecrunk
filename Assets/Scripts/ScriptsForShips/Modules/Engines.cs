﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : Module
{

    override protected void Fire()
    {
        //Allow for ship movement for a time
        
    }

    public override void Turn(bool clockwise)
    {
        DefaultTurn(clockwise);
    }

}
