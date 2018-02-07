using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]

public class Player : MonoBehaviour {

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float highJumpMultiplier = 0.75f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    [SerializeField]
    float sprintSpeed = 18;
    [SerializeField]
    float moveSpeed = 6;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnStick;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;

    float velocityXSmoothing;

    Vector2 velocity;

    Controller2D controller;
    Vector2 directionalInput;
    bool wallSliding;
    //bool sprinting;
    int wallDirX;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        //Hyppäämisen käytetty kaava 
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        //print("Gravity: " + gravity + ", Jump Velocity: " + maxJumpVelocity);
    }

    void Update()
    {
        CalculateVelocity();
        HandleWallSliding();

        //varsinainen liikkumismetodi (löytyy controller2D-skriptistä)
        controller.Move(velocity * Time.deltaTime, directionalInput);

        //Jos pelaajan ylä- tai alapuolella on este, nopeus y-suunnassa on 0
        if (controller.collisions.above || controller.collisions.below)
        {
            if(controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }

    public void SetDirectionalInput (Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        //erityyppiset seinähypyt
        if (wallSliding)
        {
            //kun pelaaja painaa liikkumista seinään päin
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            //kun pelaaja ei paina liikkumisnappia
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            //kun pelaaja painaa liikkumista seinästä poispäin
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }

        //ylöspäin ja hyppyä painaessa korkeampi hyppy
        if (controller.collisions.below && directionalInput.y > 0)
        {
           
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y * ((1 + directionalInput.y) * highJumpMultiplier);

                if (directionalInput.x == -Mathf.Sign(controller.collisions.slopeNormal.x))
                {
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x / 6;
                }
                else
                {
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x*2;
                }

                //Jos haluaisi että liian jyrkkää seinää ylös ei voisi hyppiä ylös niin käytettäisiin seuraavaa
                /* if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                 {
                     velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y * ((1 + directionalInput.y) * highJumpMultiplier);
                     velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                 }*/
            }
            else
            {
                velocity.y = maxJumpVelocity * ((1 + directionalInput.y) * highJumpMultiplier);
            }
        }
        //tavallinen hyppy
        else if (controller.collisions.below)
        {
            if(controller.collisions.slidingDownMaxSlope)
            {
                velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;

                if(directionalInput.x == -Mathf.Sign(controller.collisions.slopeNormal.x))
                {
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x/4;
                }
                else
                {
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
                

                //Jos haluaisi että liian jyrkkää seinää ylös ei voisi hyppiä ylös niin käytettäisiin seuraavaa
                /*if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                {
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }*/
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }  
        //print(directionalInput);
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    public void OnSprintDown()
    {
        moveSpeed = sprintSpeed;
    }

    public void OnSprintUp()
    {
        moveSpeed = 6;
    }

    void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;

        //Ehdot seinään tarttumiselle
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;
            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnStick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnStick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnStick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnStick = wallStickTime;
            }
        }
    }

    void CalculateVelocity()
    {
        //horisontaalinen liike pelaajan inputin mukaan
        float targetVelocityX = directionalInput.x* moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //painovoima
        velocity.y += gravity * Time.deltaTime;
        //print(velocity.x);
    }

    public void DestroyPlayer()
    {
        Destroy(this.gameObject);
        Debug.Log("Hello world!");
    }

}
