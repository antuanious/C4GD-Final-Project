using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime : MonoBehaviour
{
    Animator anim;

    PlayerManager picks;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        picks = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float pp = picks.speeed;
        anim.SetFloat("Dad", pp);
    }
}
