using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Module : MonoBehaviour
{

    protected bool m_LockedIn;
    protected bool m_Firing;
    protected float m_Charge;
    protected float m_MaxCharge;
    protected float m_ChargeTime;
    protected float m_ChargeDownTime;
    protected Rigidbody m_ModuleRB;
    protected Collider m_ModuleCollider;
    protected Crunk m_CurrentCrunk = null;
	protected Ship m_ParentShip = null;

    public void LockIn(Crunk c)
    {
        m_CurrentCrunk = c;

		if (m_CurrentCrunk != null)
		{
			m_CurrentCrunk.Mover.Stationary = true;
		}

		m_LockedIn = true;
    }

    public void LockOut()
    {
		if (m_CurrentCrunk != null)
		{
			m_CurrentCrunk.Mover.Stationary = false;
		}

		m_CurrentCrunk = null;
        m_LockedIn = false;
    }

    public bool IsLockedIn()
    {
        return m_LockedIn;
    }

    public void PumpUp()
    {
        if (!m_Firing && m_LockedIn)
        {
            m_Charge += m_MaxCharge / m_ChargeTime;
        }
    }

    protected void PumpDown()
    {
        if (m_Firing)
        {
            m_Charge -= m_MaxCharge / m_ChargeDownTime;
        }
    }

    protected void Update()
    {
        if (!m_Firing)
        {
            if (m_Charge > m_MaxCharge)
            {
                //start firing
                StartCoroutine(FireRoutine());
            }
        } else
        {
            if (m_Charge < 0f)
            {
                //stop firing
                StopFiring();
            }
        }
    }

    protected void StopFiring()
    {
        m_Charge = 0f;
        m_Firing = false;
    }

    protected IEnumerator FireRoutine()
    {
        m_Firing = true;
        while (m_Charge > 0f)
        {
            PumpDown();
            yield return null;
        }
        m_Firing = false;
    }

    abstract protected IEnumerator Fire();
}
