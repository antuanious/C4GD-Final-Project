using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed = 2f;
    public GameObject enemy;
    public Dictionary<int, EnemyData> enemies = new Dictionary<int, EnemyData>();
    public GameObject[] enemies1;

    public float rotationOffset = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance != null)
        {
            Vector2 dir = (Vector2)PlayerManager.instance.transform.position - (Vector2)transform.position;
            rb.velocity = dir.normalized * speed;

            // Make the enemy face the player:
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("jheheueheuh");
        }
    }
}
