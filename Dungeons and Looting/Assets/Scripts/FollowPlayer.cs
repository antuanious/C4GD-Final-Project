using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        if (PlayerManager.instance != null)
        {
            transform.position = new Vector3(PlayerManager.instance.transform.position.x, //take the player's x position
            PlayerManager.instance.transform.position.y, //take the player's y position
            transform.position.z); //keep the original z position for the camera
        }

    }
}


