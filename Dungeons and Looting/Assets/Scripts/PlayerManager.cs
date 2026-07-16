
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerManager instance;
    public GameObject plr;
    public float speed = 5f;
    public Image HealthBar;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D   >();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        HealthBar.rectTransform.sizeDelta = new Vector2(PlayerData.plrHp, 10);
        
        if(plr != null)
        {
            float HorizontalInput = Input.GetAxisRaw("Horizontal");
            float VerticalInput = Input.GetAxisRaw("Vertical");
            Vector2 inputDir = new Vector2(HorizontalInput, VerticalInput).normalized;
            rb.velocity = inputDir.normalized * speed;
        }
        if(PlayerData.plrHp <= 0)
        {
            Destroy(plr);
            GameUIHandler.instance.EnableEndScreen();
        }

        
    }


}
