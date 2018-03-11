using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDeconstructor : MonoBehaviour {

    //Tehnyt Joona Jäppinen
    //Palauttaa pelikentän palasen takaisin obejectpoolin kun sitä ei enää tarvita.

    [SerializeField]
    LayerMask WalloDCollision; //Layermaski jonka avulla huomioidaan vain pelaajaa jahtaavan peliobjectin collisio

    GameObject WalloD;

    [SerializeField]
    float rayLength = 20f;

    [SerializeField]
    BoxCollider2D collider;

    bool hasBeenTriggered = false;

    void Start()
    {
        WalloD = GameObject.FindGameObjectWithTag("WallODeath");
    }

    void Update()
    {
        Debug.DrawRay(new Vector2(collider.bounds.min.x, collider.bounds.min.y), transform.TransformDirection(Vector2.up * rayLength), Color.yellow);
        RaycastHit2D trigger = Physics2D.Raycast(new Vector2(collider.bounds.min.x, collider.bounds.min.y), transform.TransformDirection(Vector2.up), rayLength, WalloDCollision);

        if (trigger && !hasBeenTriggered)
        {
            Debug.Log("Initiating deconstruction sequence...");
            hasBeenTriggered = true;
            StartCoroutine(CellDeconstruct());
        }
    }

    //Kun Pelaajaa jahtaava peliobjecti osuu raycastiin se käynnistää ajastimen jonka loputtua pelikentänpalanen palautetaan takaisin objectpoolin
    IEnumerator CellDeconstruct()
    {  
        yield return new WaitForSeconds(10);
        transform.root.gameObject.GetComponent<CellSpawnTrigger>().TriggerSwitch();
        transform.root.gameObject.SetActive(false);
        hasBeenTriggered = false;
    }
}
