﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ShipDamage : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Ship m_Ship;
    [SerializeField]
    private int hp;
    private int max_hp;
    [SerializeField]
    private float immuneFromDamageTime;
    private float timeSinceLastDamage;
    private bool currentlyImmuneToDamage;
    public bool immuneFromShield;

    private void Awake()
    {
        max_hp = hp;
    }
    private void OnTriggerEnter(Collider other)
    {
        Projectile p = other?.GetComponent<Projectile>();
        if (p != null)
        {
            if (p.m_Team != m_Ship.GetTeam())
            {
                if (!currentlyImmuneToDamage && !immuneFromShield)
                {
                    TakeDamage();
                }
            }
        }
    }
    public int GetTeam()
    {
        return m_Ship.GetTeam();
    }

    private void TakeDamage()
    {
        Debug.Log("Took damage on ship HP is " + hp);
        m_Ship.KnockOffRandomModule();
        hp -= 1;
        CheckIfShipDead();
        timeSinceLastDamage = 0;
        currentlyImmuneToDamage = true;
    }

    private void HandleDamageImmunity()
    {
        if (currentlyImmuneToDamage)
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage > immuneFromDamageTime)
            {
                currentlyImmuneToDamage = false;
            }
        }
    }

    private void Update()
    {
        image.fillAmount = (float)hp / (float)max_hp;
        HandleDamageImmunity();
    }

    private void CheckIfShipDead()
    {
        if(hp <= 0)
        {
            /*💀*/KillShipDie();/*💀*/
        }
    }
    private void KillShipDie()
    {
        m_Ship.Kill();/*💀*//*💀*//*💀*//*💀*//*💀*/
    }
}
