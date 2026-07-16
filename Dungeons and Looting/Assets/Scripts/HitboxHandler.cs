using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitboxHandler : MonoBehaviour
{
    public GameObject enemy1;
    public EnemyData enemy;
    public float damageDelay = 0.4f;
    private void Start()
    {
        enemy = enemy1.GetComponent<EnemyData>();
        
        //damageDelay = enemy.enemyAttackRate - 0.1f;
    }

    private readonly Dictionary<Collider2D, Coroutine> damageCoroutines =
        new Dictionary<Collider2D, Coroutine>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("Something entered the hitbox.");

        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Do not start another timer for the same collider.
        if (damageCoroutines.ContainsKey(other))
        {
            return;
        }

        Coroutine damageCoroutine =
            StartCoroutine(DelayedDamageCoroutine(other));

        damageCoroutines.Add(other, damageCoroutine);
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!damageCoroutines.TryGetValue(other, out Coroutine damageCoroutine))
        {
            return;
        }

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }

        damageCoroutines.Remove(other);
    }

    private IEnumerator DelayedDamageCoroutine(Collider2D target)
    {
        yield return new WaitForSeconds(damageDelay);

        // The target may have been destroyed during the delay.
        if (target == null)
        {
            yield break;
        }

        // Confirm that the timer was not cancelled by OnTriggerExit2D.
        if (!damageCoroutines.ContainsKey(target))
        {
            yield break;
        }

        StartCoroutine(ApplyDamageToTarget(target, enemy.damage));
        Debug.Log("Delayed damage coroutine finished.");

        damageCoroutines.Remove(target);
    }
    bool isTakingDmg = false;
    private IEnumerator ApplyDamageToTarget(Collider2D target, float amount)
    {

        if (target == null || isTakingDmg)
        {
            yield return null;
        }
        else
        {
            isTakingDmg = true;
            float defense = PlayerData.plrdef;
            float effectiveDamage = Mathf.Max(0f, amount - defense);

            PlayerData.plrHp =
                Mathf.Max(0f, PlayerData.plrHp - effectiveDamage);

            Debug.Log(
                $"Applied {effectiveDamage} damage. Player HP: {PlayerData.plrHp}"
            );
            yield return new WaitForSeconds(0.4f);
            isTakingDmg = false;
        }
    }

    private void OnDisable()
    {
        foreach (Coroutine damageCoroutine in damageCoroutines.Values)
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }

        damageCoroutines.Clear();
    }
}