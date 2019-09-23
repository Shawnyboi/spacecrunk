using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    protected Rigidbody m_Rigidbody;
    protected SphereCollider m_Collider;
    public int m_Team;
    public GameObject m_ProjectileMesh;
    public GameObject m_HitParticles;
    public float m_ParticleLifetime = .5f;

    [SerializeField]
    protected float m_Speed = 10f;
    [SerializeField]
    protected float m_Lifetime = 7f;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<SphereCollider>();
        m_Rigidbody.velocity = this.transform.forward * m_Speed;
        Destroy(this.gameObject, m_Lifetime);
    }

    public void ConfirmHit()
    {
        StartCoroutine(LaserHit());
    }

    private IEnumerator LaserHit()
    {
        m_ProjectileMesh.SetActive(false);
        m_HitParticles.SetActive(true);
        yield return new WaitForSeconds(m_ParticleLifetime);
        Destroy(this.gameObject);
    }
}
