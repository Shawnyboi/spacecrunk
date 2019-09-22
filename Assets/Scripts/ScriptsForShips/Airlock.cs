using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_ShipImAttachedTo;
    [SerializeField]
    protected float m_AirlockForce;
    [SerializeField]
    protected Transform m_GoThroughAirlockVector;

    public void LeaveShip(CrunkMover cm)
    {
        cm.ApplyExternalForce(m_AirlockForce * m_GoThroughAirlockVector.forward);
        cm.setGrounded(false);
        cm.crunk.parentShip = null;
		Debug.Log("LeaveShip");
    }

    public void EnterShip(CrunkMover cm)
    {
        cm.ApplyExternalForce(-m_AirlockForce * m_GoThroughAirlockVector.forward);
        cm.setGrounded(true);
        cm.crunk.parentShip = m_ShipImAttachedTo.GetComponent<Ship>();
		Debug.Log("EnterShip");

	}
}
