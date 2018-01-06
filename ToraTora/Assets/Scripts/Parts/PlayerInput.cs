using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerControl player;

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        player.SetDirection((new Vector2(h, v)).normalized);

        if(Input.GetButtonDown("Fire"))
        {
            player.Shoot();
        }
    }
}
