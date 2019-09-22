using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Module
{
    [SerializeField]
    protected Transform m_FirePoint;
    [SerializeField]
    protected GameObject m_Projectile;
    

    private float rechargeTime = .025f;
    private float timePassedSinceLastShot = 0f;

    override protected void Fire()
    {
        if (timePassedSinceLastShot > rechargeTime)
        {
            GameObject projectile = Instantiate(m_Projectile, m_FirePoint.position, m_FirePoint.rotation);
            timePassedSinceLastShot = 0f;
        }
        else
        {
            timePassedSinceLastShot += Time.deltaTime;
        }
    }

    public override void Turn(bool clockwise)
    {
        DefaultTurn(clockwise);
    }

    /* uncomment for testing*/
    /*private void Start()
    {
        m_TestFire();
    }
    public void m_TestFire()
    {
        m_Charge = m_MaxCharge;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Turn(false);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Turn(true);
        }
    }
    */
}