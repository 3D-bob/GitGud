﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player))]

//Made by Tuomas Ahonen

public class PlayerInput : MonoBehaviour {

    Player player;
    Animator anim;

	void Start ()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        //Animaattorin päivitys
        int dir = 0;

        if(directionalInput.x < 0)
        {
            dir = -1;
        }
        if (directionalInput.x > 0)
        {
            dir = 1;
        }
        if(directionalInput.x == 0)
        {
            dir = 0;
        }

        anim.SetInteger("Input", dir);

        //Hyppy
        if(Input.GetButtonDown("Jump"))
        {
            player.OnJumpInputDown();
        }

        if (Input.GetButtonUp("Jump"))
        {
            player.OnJumpInputUp();
        }

        //Sprintti
        if (Input.GetButtonDown("Fire3"))
        {
            player.OnSprintDown();
        }

        if (Input.GetButtonUp("Fire3"))
        {
            player.OnSprintUp();
        }

        /*if(directionalInput.y > 0 && Input.GetButtonDown("Jump"))
        {

        }*/

    }
}
