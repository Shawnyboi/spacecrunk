using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Ship))]
public class ShipDamage : MonoBehaviour
{
    [SerializeField]
    private Ship m_Ship;
    [SerializeField]
    private int hp;
    [SerializeField]
    private float immuneFromDamageTime;
    private float timeSinceLastDamage;
    private bool currentlyImmuneToDamage;
    
    private void OnTriggerEnter(Collider other)
    {
        Projectile p = other?.GetComponent<Projectile>();
        if (p.m_Team == m_Ship.GetTeam())
        {
            if (!currentlyImmuneToDamage)
            {
                TakeDamage();
            }
        }
    }

    private void TakeDamage()
    {
        Debug.Log("Took damage on ship HP is " + hp);
        m_Ship.KnockOffRandomModule();
        hp -= 1;
        timeSinceLastDamage = 0;
        currentlyImmuneToDamage = true;
    }

    private void HandleDamageImmunity()
    {
        if (currentlyImmuneToDamage)
        {
            timeSinceLastDamage += Time.deltaTime;
            if(timeSinceLastDamage > immuneFromDamageTime)
            {
                currentlyImmuneToDamage = false;
            }
        }
    }

    private void Update()
    {
        HandleDamageImmunity();
    }
}
