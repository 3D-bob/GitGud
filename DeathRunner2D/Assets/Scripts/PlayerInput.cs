using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

	void Start ()
    {
        player = GetComponent<Player>();
	}
	
	void Update () {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

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
