﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Made by Tuomas Ahonen (controllerin pohja on tehty tutoriaalin mukaisesti, mutta siihen on lisätty ominaisuuksia ja tehty paljon muutoksia)

public class Controller2D : RaycastController {

    public float maxSlopeAngle = 80;

    GameObject other;

    

    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;

    public ScoreCounter sc;

    public override void Start()
    {
        base.Start();
        collisions.faceDir = 1;
    }

    public void Move(Vector2 moveAmount, Vector2 input)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.velocityOld = moveAmount;
        playerInput = input;

        if (moveAmount.y < 0)
        {
            DescendSlope(ref moveAmount);
        }

        if (moveAmount.x != 0)
        {
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
        }

        HorizontalCollisions(ref moveAmount);
        
        if(moveAmount.y != 0)
        {
            VerticalCollisions(ref moveAmount);
        }
        
        transform.Translate(moveAmount);
    }

    void HorizontalCollisions(ref Vector2 moveAmount)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        if(Mathf.Abs(moveAmount.x) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);
            //Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                //Jos platformin läpi voi hypätä tai pudota ->
                if(hit.distance == 0)
                {
                    continue;
                }

                //Palikat vaihtelloo värejä, kun ossuupi
                if (hit.collider.tag == "Corruptable" || hit.collider.tag == "Passable")
                {
                    other = hit.collider.gameObject;
                    Corrupt(ref other);
                }

                //Kuolo korjaa, jos tämmössiin törmää
                if (hit.collider.tag == "DeathAwaits")
                {
                    this.gameObject.SetActive(false);
                }

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if(i == 0 && slopeAngle <= maxSlopeAngle)
                {
                    if(collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        moveAmount = collisions.velocityOld;
                    }

                    float distanceToSlopeStart = 0;
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        moveAmount.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref moveAmount, slopeAngle, hit.normal);
                    moveAmount.x += distanceToSlopeStart * directionX;
                }
                
                if(!collisions.climbingSlope || slopeAngle > maxSlopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                } 
            }
        }
    }

    void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);
            //Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                //Tiettyjen esteiden läpi menemeninen 
                if(hit.collider.tag == "Passable")
                {
                    if(directionY == 1 || hit.distance == 0)
                    {
                        continue;
                    }
                    if(playerInput.y == -1)
                    {
                        continue;
                    }
                }

                //Palikat vaihtelloo värejä, kun ossuupi
                if (hit.collider.tag == "Corruptable" || hit.collider.tag == "Passable")
                {
                    other = hit.collider.gameObject;
                    Corrupt(ref other);
                }

                //Kuolo korjaa, jos tämmössiin törmää
                if (hit.collider.tag == "DeathAwaits")
                {
                    this.gameObject.SetActive(false);
                    
                }


                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if(collisions.climbingSlope)
                {
                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
                       
                }


                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x);
            rayLength = Mathf.Abs(moveAmount.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(slopeAngle != collisions.slopeAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                    collisions.slopeNormal = hit.normal;
                }
            }
        }
    }

    //palikat vaihtelloo värejä
    void Corrupt(ref GameObject other)
    {
        SpriteRenderer SR;
        ParticleSystem PS;

        //Jos on partikkelisysteemi, niin kääpi tälleensä
        if(other.GetComponent<ParticleSystem>())
        {
            PS = other.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = PS.main;
            main.startColor = Color.blue;
        }
       
        //Spriten värin vaihtaminen
        if(other.GetComponent<SpriteRenderer>().color != Color.blue)
        {
            sc.UpdateScore();
            SR = other.GetComponent<SpriteRenderer>();
            SR.color = Color.blue;
        }
        
    }

    //Ylämäkeen kiipeäminen
    void ClimbSlope(ref Vector2 moveAmount, float slopeAngle, Vector2 slopeNormal)
    {
        float moveDistance = Mathf.Abs(moveAmount.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if(moveAmount.y <= climbVelocityY)
        {
            moveAmount.y = climbVelocityY;
            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
            collisions.slopeNormal = slopeNormal;
        }     
    }

    //Alamäkeen laskeutuminen
    void DescendSlope(ref Vector2 moveAmount)
    {
        RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);
        RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);

        if(maxSlopeHitLeft ^ maxSlopeHitRight)
        {
            SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount);
            SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount);
        }
        
        if(!collisions.slidingDownMaxSlope)
        {
            float direcetionX = Mathf.Sign(moveAmount.x);
            Vector2 rayOrigin = (direcetionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != 0 && slopeAngle <= maxSlopeAngle)
                {
                    if (Mathf.Sign(hit.normal.x) == direcetionX)
                    {
                        if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
                        {
                            float moveDistance = Mathf.Abs(moveAmount.x);
                            float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                            moveAmount.y -= descendVelocityY;

                            collisions.slopeAngle = slopeAngle;
                            collisions.descendingSlope = true;
                            collisions.below = true;
                            collisions.slopeNormal = hit.normal;
                        }
                    }
                }
            }     
        }
    } 

    void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount)
    {
        if(hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeAngle> maxSlopeAngle)
            {
                moveAmount.x = hit.normal.x * (Mathf.Abs(moveAmount.y) - hit.distance) / Mathf.Tan(slopeAngle * Mathf.Deg2Rad);

                collisions.slopeAngle = slopeAngle;
                collisions.slidingDownMaxSlope = true;
                collisions.slopeNormal = hit.normal;
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public float slopeAngle, slopeAngleOld;
        public Vector2 slopeNormal;
        public Vector2 velocityOld;

        public bool climbingSlope;
        public bool descendingSlope;
        public bool slidingDownMaxSlope;

        public int faceDir;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;
            slopeNormal = Vector2.zero;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}
