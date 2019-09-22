using UnityEngine;

[RequireComponent(typeof(Collider))]
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
        if (p != null)
        {
            if (p.m_Team == m_Ship.GetTeam())
            {
                if (!currentlyImmuneToDamage)
                {
                    TakeDamage();
                }
            }
        }
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
