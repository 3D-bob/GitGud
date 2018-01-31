using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath : MonoBehaviour {

    [SerializeField]
    float speed = 100f;
    [SerializeField]
    float speedMod = 0.01f;

    [SerializeField]
    LayerMask playerCollision;
    
    GameObject player;
    float playerY;
    float wallX;

    public BoxCollider2D collider;
    public RaycastPoints raycastPoints;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update ()
    {
        playerY = player.transform.position.y;
        
        this.gameObject.transform.Translate(speed * Time.deltaTime,0,0);
        speed += speedMod;

        wallX = this.gameObject.transform.position.x;
        this.gameObject.transform.position = new Vector2(wallX,playerY);

        Bounds bounds = collider.bounds;
        raycastPoints.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastPoints.bottomRight = new Vector2(bounds.max.x, bounds.min.y);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        Debug.DrawRay(raycastPoints.topRight, Vector2.right * 2, Color.blue);
    }

    private void FixedUpdate()
    {
        
    }

    public struct RaycastPoints
    {
        public Vector2 topRight;
        public Vector2 bottomRight;
    }



}
