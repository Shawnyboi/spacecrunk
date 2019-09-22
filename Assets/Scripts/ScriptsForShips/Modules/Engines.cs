using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : Module
{
    [SerializeField]
    protected Transform m_ForcePoint;
    [SerializeField]
    protected float m_EngineForceMagnitude;
    override protected void Fire()
    {
        //Allow for ship movement for a time     
        MoveShip();
    }

    public override void Turn(bool clockwise)
    {
        DefaultTurn(clockwise);
    }


    protected void MoveShip()
    {
        Rigidbody parentRB = m_ParentShip.GetComponent<Rigidbody>();
        parentRB.AddForceAtPosition(m_ForcePoint.forward * m_EngineForceMagnitude, m_ForcePoint.position);
    }

    private void TestFire()
    {
        m_Charge = m_MaxCharge;
    }

    

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Turn(false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Turn(true);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            TestFire();
        }
    }
}
