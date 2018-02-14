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
    float playerX;
    float wallX;

    const float dstBetweenRays = .25f;

    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [SerializeField]
    float rayLength = 0.1f;

    public BoxCollider2D collider;
    public RaycastPoints raycastPoints;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CalculateRaySpacing();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateRaycastOrigins();
        playerY = player.transform.position.y;
        playerX = player.transform.position.x;
        
        //this.gameObject.transform.Translate(speed * Time.deltaTime,0,0);
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, transform.position = new Vector2(playerX, playerY), speed * Time.deltaTime);
        //speed += speedMod;

        if (!(speed >= 15))
        {
            speed += speedMod;
        }

        Debug.Log(speed);


        //tracks the players location on x axis
        wallX = this.gameObject.transform.position.x;
        //this.gameObject.transform.position = new Vector2(wallX,playerY);
        //this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, transform.position = new Vector2(wallX, playerY), 0.02f);

        //mestarikoodaaja Tuomas teki tämän raycastin, koska muilla ei taidot riittäny
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = raycastPoints.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, playerCollision);

            if(hit)
            {
                player.GetComponent<Player>().DestroyPlayer();
            }
            Debug.DrawRay(rayOrigin, Vector2.right * rayLength, Color.blue);
        }
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;

        raycastPoints.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastPoints.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / dstBetweenRays);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastPoints
    {
        public Vector2 topRight;
        public Vector2 bottomRight;
    }

}
