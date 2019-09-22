using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
abstract public class Module : MonoBehaviour
{

    protected bool m_LockedIn;
    protected bool m_Firing = false;
    protected float m_Charge;
    protected const float m_MaxCharge = 1;
    [SerializeField]
    protected float m_TurnSpeed;
    [SerializeField]
    protected float m_MaxArc;
    [SerializeField]
    protected float m_ChargeTime;
    [SerializeField]
    protected float m_ChargeDownTime;
    protected Rigidbody m_ModuleRB;
    protected Collider m_ModuleCollider;
    protected Crunk m_CurrentCrunk = null;
	public Crunk CurrentCrunk => m_CurrentCrunk;
    [SerializeField] //serializing for testing purposes
    protected Ship m_ParentShip;
    protected Rigidbody m_Rigidbody;
	public Collider ModuleCollider = null;

    public void AttachToShip(Ship s) { m_ParentShip = s; }

    public void RemoveFromShip() { m_ParentShip = null; }
    public void LockIn(Crunk c)
    {
        m_CurrentCrunk = c;

		if (m_CurrentCrunk != null)
		{
            if (m_CurrentCrunk.parentShip == m_ParentShip)
            {
                m_CurrentCrunk.Mover.Stationary = true;
            }
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

    public bool PumpUp()
    {
        if (!m_Firing && m_LockedIn)
        {
            m_Charge += m_MaxCharge / m_ChargeTime * Time.deltaTime;
			return true;
        }
		return false;
    }

    protected void PumpDown()
    {
        if (m_Firing)
        {
            m_Charge -=  m_MaxCharge / m_ChargeDownTime * Time.deltaTime;
        }
    }

     void Update()
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

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.angularVelocity = new Vector3(0,Random.Range(-1f, 1f), 0);
        m_Rigidbody.velocity = new Vector3(Random.Range(-.5f, .5f), 0f, Random.Range(-.5f, .5f));
    }


    protected void StartFiring()
    {
        m_Firing = true;
        StartCoroutine(FireRoutine());
    }

    virtual protected void StopFiring()
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
    abstract public void Turn(bool clockwise);

    protected void DefaultTurn(bool clockwise)
    {

        float angle = transform.localRotation.eulerAngles.y;
        if(angle > 180)
        {
            angle = angle - 360;
        }

        if (clockwise) {
            if (angle < m_MaxArc)
            {
                transform.Rotate(transform.up, m_TurnSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            if (angle > -m_MaxArc)
            {
                transform.Rotate(transform.up, -m_TurnSpeed * Time.fixedDeltaTime);
            }
        }
	}
}
