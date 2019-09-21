using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Module : MonoBehaviour
{

    protected bool m_LockedIn;
    protected bool m_Firing = false;
    protected float m_Charge;
    protected const float m_MaxCharge = 1;
    [SerializeField]
    protected float m_ChargeTime;
    [SerializeField]
    protected float m_ChargeDownTime;
    protected Rigidbody m_ModuleRB;
    protected Collider m_ModuleCollider;
    protected Crunk m_CurrentCrunk = null;

    public void LockIn(Crunk c)
    {
        m_CurrentCrunk = c;
        m_LockedIn = true;
    }

    public void LockOut()
    {
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
            m_Charge += m_MaxCharge / m_ChargeTime * Time.deltaTime;
        }
    }

    protected void PumpDown()
    {
        if (m_Firing)
        {
            m_Charge -=  m_MaxCharge / m_ChargeDownTime * Time.deltaTime;
        }
    }

    protected void Update()
    {
        if (!m_Firing)
        {
            if (m_Charge >= m_MaxCharge)
            {
                //start firing
                Debug.Log("Starting firing");
                StartFiring();
            }
        }
    }

    protected void StartFiring()
    {
        m_Firing = true;
        StartCoroutine(FireRoutine());
    }

    protected void StopFiring()
    {
        m_Charge = 0f;
        m_Firing = false;
    }

    protected IEnumerator FireRoutine()
    {
        while (m_Charge > 0f)
        {
            Fire();
            PumpDown();
            yield return null;
        }
        StopFiring();
        yield return null;
    }

    abstract protected void Fire();
}
