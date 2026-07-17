using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour {


    // Start is called before the first frame update
    public string enemyName;
    public float enemyHealth;
    public float speed;
    public float enemyAttackRate;
    public float damage;
    public float enemyHPOG;

    bool isTakingDmg = false;


    public void TakeDamage(float damage)
    {
        if (!isTakingDmg)
        {
            StartCoroutine(HandleDmgEvent(damage, .2f));
        }
    }
    IEnumerator HandleDmgEvent(float damage, float time)
    {
        isTakingDmg=true;
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(time);
        isTakingDmg = false;
    }
}
