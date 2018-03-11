using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawnTrigger : MonoBehaviour {

    //Tehnyt Joona Jäppinen
    //Generoi uuden kenttäpalasen kun pelaaja osuu raycastiin

    [SerializeField]
    LayerMask playerCollision; //Layermaski jonka avulla huomioidaan vain collisiot pelaaja hahmon kanssa.

    GameObject Spawner;
    GameObject player;
    GameObject WallODeath;

    [SerializeField]
    float rayLength = 20f;

    [SerializeField]
    BoxCollider2D collider;

    bool hasBeenTriggered = false;

    void Start ()
    {
        WallODeath = GameObject.FindGameObjectWithTag("WallODeath");
        Spawner = GameObject.FindGameObjectWithTag("spawner");
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update ()
    {
        Debug.DrawRay(new Vector2(collider.bounds.min.x, collider.bounds.min.y), transform.TransformDirection(Vector2.up * rayLength), Color.yellow);
        RaycastHit2D trigger = Physics2D.Raycast(new Vector2(collider.bounds.min.x, collider.bounds.min.y), transform.TransformDirection(Vector2.up), rayLength, playerCollision);

        if (trigger && !hasBeenTriggered)
        {
            Spawner.GetComponent<LvlSpwng2>().generateLevel(); //kutsuu level spawneria ja käskee sitä hakemaan objectpoolista ei käytössä olevan tasopalasen ja asettamaan sen pelikenttään.
            //Debug.Log("Triggered");
            hasBeenTriggered = true; //booli jonka avulla tarkistetään onko pelaaja vielä aktivoinut pelikentän generointia.
        }      
    }

    public void TriggerSwitch()
    {
        hasBeenTriggered = false;
    }
}
