using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    protected Rigidbody m_Rigidbody;
    protected SphereCollider m_Collider;

    [SerializeField]
    protected float m_Speed = 10f;
    [SerializeField]
    protected float m_Lifetime = 5f;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<SphereCollider>();
        m_Rigidbody.velocity = this.transform.forward * m_Speed;
        Destroy(this, m_Lifetime);
    }
}
