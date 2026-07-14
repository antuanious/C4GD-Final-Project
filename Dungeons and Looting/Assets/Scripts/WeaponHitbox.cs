using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CombatManager.instance.attacking)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyData>().TakeDamage(CombatManager.instance.currentWeaponDamage);
                print(collision.GetComponent<EnemyData>().enemyHealth);
            }
        }
        
    }
}