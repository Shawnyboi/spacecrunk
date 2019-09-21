using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Module
{
    private bool m_HasFiredProjectile = false;
    [SerializeField]
    protected GameObject m_Projectile;
    private float m_MaxRotation;
    private float m_ProjectileSpeed;

    public bool HasFired() { return m_HasFiredProjectile; }
    override protected IEnumerator Fire()
    {
        while (m_Firing)
        {
            if (!m_HasFiredProjectile)
            {
                GameObject projectile = Instantiate(m_Projectile, this.m_ModuleRB.transform.position, this.m_ModuleRB.rotation);
                //add velocity to projectile
                m_HasFiredProjectile = true;
            }
            yield return null;
        }
        yield return null;

    }
}