using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Module
{
    [SerializeField]
    protected MeshRenderer m_ForceField;
    public GameObject m_CurrentForceField;
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
		if (m_CurrentForceField == null)
		{
            m_CurrentForceField.SetActive(true);
            m_ParentShip.shipDamage.immuneFromShield = true;
			//GameObject ff = Instantiate(m_ForceFieldPrefab, m_ShieldPoint, false);
			//ff.transform.parent = m_ShieldPoint;
			//m_CurrentForceField = ff;
            //PlayFireSound();
		}
    }

    protected void TurnShieldOff()
    {
        m_CurrentForceField.SetActive(false);
        m_ParentShip.shipDamage.immuneFromShield = false;
    }

}
