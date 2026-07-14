using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Movement and existing fields
    public Rigidbody2D rb;
    public EnemyData enemy;

    public float rotationOffset = 0f;

    // Attack / hitbox fields
    public GameObject hitboxPrefab;      // assign a hitbox prefab in the Inspector
    public float attackInterval = 1f;    // seconds between attacks
    public float hitboxLifetime = 0.5f;  // seconds before the spawned hitbox is auto-destroyed
    private float attackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyData>();
        attackTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance != null)
        {
            Vector2 dir = (Vector2)PlayerManager.instance.transform.position - (Vector2)transform.position;
            rb.velocity = dir.normalized * enemy.speed;

            // Make the enemy face the player:
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
        }

        // Attack timer logic: spawn a hitbox every `attackInterval` seconds
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            SpawnHitbox();
            attackTimer = 0f;
        }
    }

    private void SpawnHitbox()
    {
        if (hitboxPrefab == null)
            return;

        // Spawn slightly in front of the enemy so it doesn't overlap the enemy collider.
        float offsetDistance = 2f;
        Vector3 forward = transform.right; // facing direction given current rotation
        Vector3 spawnPos = transform.position + forward * offsetDistance;

        GameObject hitbox = Instantiate(hitboxPrefab, spawnPos, transform.rotation);
        if (hitboxLifetime > 0f)
        {
            Destroy(hitbox, hitboxLifetime);
        }
    }

    
}
