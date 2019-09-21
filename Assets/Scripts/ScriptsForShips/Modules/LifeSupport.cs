using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSupport : Module
{
    [SerializeField]
    protected float m_OxygenRestoreRate = 5;
    protected override void Fire()
    {
        //Fill up the oxygen in the ship or on the players or however
        AddOxygenToShip();
        
    }

    public override void Turn(bool clockwise)
    {
        //life supports don't turn
    }

    protected void AddOxygenToShip()
    {
        m_ParentShip.AddOxygen(m_OxygenRestoreRate);
    }
}
