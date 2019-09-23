using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Module
{
    
    [SerializeField]
    protected bool m_ShieldIsOn;
    override protected void Fire()
    {
		TurnShieldOn();
    }

    public override void Turn(bool clockwise)
    {
        DefaultTurn(clockwise);
    }

    override protected void StopFiring()
    {

        base.StopFiring();
        TurnShieldOff();
    }

    protected void TurnShieldOn()
    {
		
        m_ParentShip.forcefield.SetActive(true);
        m_ParentShip.shipDamage.immuneFromShield = true;
		//GameObject ff = Instantiate(m_ForceFieldPrefab, m_ShieldPoint, false);
		//ff.transform.parent = m_ShieldPoint;
		//m_CurrentForceField = ff;
        //PlayFireSound();
		
    }

    protected void TurnShieldOff()
    {
        m_ParentShip.forcefield.SetActive(false);
        m_ParentShip.shipDamage.immuneFromShield = false;
    }

}
