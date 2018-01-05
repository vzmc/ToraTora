using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        player.Move(new Vector2(h, v));

        if(Input.GetButtonDown("Fire"))
        {
            player.Shoot();
        }
    }
}
