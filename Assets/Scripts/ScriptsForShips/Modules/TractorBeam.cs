using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : Module
{
    protected override void Fire()
    {
        //turn on  the tractor beam and allow for it to be aimed
    }

    public override void Turn(bool clockwise)
    {
        DefaultTurn(clockwise);
    }
}
