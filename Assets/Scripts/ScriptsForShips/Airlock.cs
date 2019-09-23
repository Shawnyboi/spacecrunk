using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    public GameObject m_ShipImAttachedTo;
    private int m_Team;
    [SerializeField]
    protected AudioSource m_AudioSource;
    [SerializeField]
    protected float m_AirlockForce;
	[SerializeField]
	protected float m_PositionOffset = 10;
    [SerializeField]
    protected Transform m_GoThroughAirlockVector;


    private void Start()
    {
        m_Team = m_ShipImAttachedTo.GetComponent<Ship>().GetTeam();
    }

    public int GetTeam() { return m_Team; }
    public void LeaveShip(CrunkMover cm)
    {
        m_AudioSource.Play();
        cm.crunk.TriggerAirlockAnimation(true);
		cm.GetComponent<Rigidbody>().position = transform.position + (m_PositionOffset * m_GoThroughAirlockVector.forward);
        cm.ApplyExternalForce(m_AirlockForce * m_GoThroughAirlockVector.forward);
        cm.setGrounded(false);
        cm.crunk.parentShip = null;
    }

    public void EnterShip(CrunkMover cm)
    {
        m_AudioSource.Play();
        cm.crunk.TriggerAirlockAnimation(false);
		cm.GetComponent<Rigidbody>().position = transform.position;
		cm.ApplyExternalForce(-m_AirlockForce * m_GoThroughAirlockVector.forward);
        cm.setGrounded(true);
        cm.crunk.parentShip = m_ShipImAttachedTo.GetComponent<Ship>();

	}
}
