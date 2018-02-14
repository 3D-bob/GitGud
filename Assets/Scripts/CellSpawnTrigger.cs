using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawnTrigger : MonoBehaviour {

    public struct RaycastPoints
    {
        public Vector2 topRight;
        public Vector2 bottomRight;
    }

    [SerializeField]
    LayerMask playerCollision;

    GameObject player;

    const float dstBetweenRays = .25f;

    [SerializeField]
    int horizontalRayCount;
    [SerializeField]
    int verticalRayCount;

    [SerializeField]
    float horizontalRaySpacing;
    [SerializeField]
    float verticalRaySpacing;

    [SerializeField]
    float rayLength = 0.1f;

    [SerializeField]
    BoxCollider2D collider;

    [SerializeField]
    RaycastPoints raycastPoints;

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

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = raycastPoints.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, playerCollision);

            if (hit)
            {
                player.GetComponent<Player>().DestroyPlayer();
            }
            Debug.DrawRay(rayOrigin, Vector2.right * rayLength, Color.yellow);
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
}
