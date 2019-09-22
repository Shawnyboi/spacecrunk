using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Module
{
    [SerializeField]
    protected GameObject m_ForceFieldPrefab;
    private GameObject m_CurrentForceField;
    [SerializeField]
    protected Transform m_ShieldPoint;
    protected bool m_ShieldIsOn;
    override protected void Fire()
    {
        //Make a shield around the shit
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
        GameObject ff = Instantiate(m_ForceFieldPrefab, m_ShieldPoint, false);
        ff.transform.parent = m_ShieldPoint;
        m_CurrentForceField = ff;
    }

    protected void TurnShieldOff()
    {
        Destroy(m_CurrentForceField);
        m_CurrentForceField = null;
    }

}
