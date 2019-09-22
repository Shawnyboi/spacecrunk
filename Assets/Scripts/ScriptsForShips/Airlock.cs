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
	protected float m_PositionOffset = 10;
    [SerializeField]
    protected Transform m_GoThroughAirlockVector;

    public void LeaveShip(CrunkMover cm)
    {
        cm.crunk.TriggerAirlockAnimation(true);
		cm.GetComponent<Rigidbody>().position = transform.position + (m_PositionOffset * m_GoThroughAirlockVector.forward);
        cm.ApplyExternalForce(m_AirlockForce * m_GoThroughAirlockVector.forward);
        cm.setGrounded(false);
        cm.crunk.parentShip = null;
    }

    public void EnterShip(CrunkMover cm)
    {
        cm.crunk.TriggerAirlockAnimation(false);
		cm.GetComponent<Rigidbody>().position = transform.position + (-m_PositionOffset * m_GoThroughAirlockVector.forward);
		cm.ApplyExternalForce(-m_AirlockForce * m_GoThroughAirlockVector.forward);
        cm.setGrounded(true);
        cm.crunk.parentShip = m_ShipImAttachedTo.GetComponent<Ship>();

	}
}
