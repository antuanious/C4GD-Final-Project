using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxHandler : MonoBehaviour
{
    public float damageAmount = 10f;
    public float damageDelay = 0.4f;

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

        ApplyDamageToTarget(target, damageAmount);

        Debug.Log("Delayed damage coroutine finished.");

        damageCoroutines.Remove(target);
    }

    private void ApplyDamageToTarget(Collider2D target, float amount)
    {
        if (target == null)
        {
            return;
        }

        float defense = PlayerData.plrdef;
        float effectiveDamage = Mathf.Max(0f, amount - defense);

        PlayerData.plrHp =
            Mathf.Max(0f, PlayerData.plrHp - effectiveDamage);

        Debug.Log(
            $"Applied {effectiveDamage} damage. Player HP: {PlayerData.plrHp}"
        );
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