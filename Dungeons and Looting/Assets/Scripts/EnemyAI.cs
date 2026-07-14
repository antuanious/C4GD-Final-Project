using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed = 2f;
    public GameObject enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance != null)
        {
            Vector2 dir = PlayerManager.instance.transform.position - transform.position;
            rb.velocity = dir.normalized * speed;

        }
    }
}
