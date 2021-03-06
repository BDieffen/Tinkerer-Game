﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedMovementScript : MonoBehaviour {
    [SerializeField]
    bool playerCanMove = false;
    float delayLength = 2f;
    float timer = 0;
    public GameObject delayedBuddy;
    Rigidbody buddyRB;
    Vector3[] loggedPositions = new Vector3[4];
    int currentlyLoggedPos = 0;
    [SerializeField]
    List<Vector3> positionsToMoveTo = new List<Vector3>();

    Animation playerAnim;
    [SerializeField]
    float currentSpeed;
    float runSpeed = 5;
    float walkSpeed = 2;
    private float moveDecayRate = 15;
    float jumpHeight = 12;
    public bool isMoving = false;
    bool isStationary = true;
    Vector3 moveThisWay;
    Rigidbody rb;
    PlayerController playerController;
    PlayerMotor playerMotor;
    public Vector3 analogDirection;
    float angle;
    //Animation footStepSounds;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //buddyRB = delayedBuddy.GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerMotor = GetComponent<PlayerMotor>();
        playerAnim = GetComponent<Animation>();
        //footStepSounds = GameObject.Find("FootstepAudio").GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        //analogDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        /*currentSpeed = buddyRB.velocity.x;
        if (playerCanMove)
        {
            MovePlayer();
        }

        //Make this looser. As of right now the player has to be exactly on top of delayBuddy for this to not trigger. Make a dead zone where the player is close enough to the delayBuddy for this to not trigger
        if (transform.position != delayedBuddy.transform.position && !playerCanMove)
        {
            DelayCountdown();
        }*/
    }

    //THIS SHOULD START THE COUNTDOWN TIL THE PLAYER STARTS MOVING TOWARDS THE delayBuddy OBJECT. DOES NOT WORK RIGHT NOW. MUST FIX
    /*public void DelayCountdown()
    {
        if(loggedPositions != null)
        {
            playerCanMove = true;
        }
        else
        {
            playerCanMove = false;
        }
    }

    //MOVES THE delayBuddy OBJECT
    public void MoveBuddy()
    {
        if (playerMotor.isMoving)
        {
            if (playerController.isRunning)
            {
                delayedBuddy.transform.Translate(((Input.GetAxisRaw("Horizontal") * Vector3.forward) + (Input.GetAxisRaw("Vertical") * Vector3.left)) * Time.deltaTime * runSpeed);
            }
            else
            {
                delayedBuddy.transform.Translate(((Input.GetAxisRaw("Horizontal") * Vector3.forward) + (Input.GetAxisRaw("Vertical") * Vector3.left)) * Time.deltaTime * walkSpeed);
            }
            //TRYING TO LOG THE delayBuddy POSITION AT CERTAIN INTERVALS OF THE DELAY INTO THE ARRAY TO HANDLE PLAYER MOVEMENT
            timer += Time.deltaTime;

            LogPositions();
        }
    }

    public void LogPositions()
    {
        if (timer >= (delayLength / loggedPositions.Length))
        {
            if (currentlyLoggedPos == 0)
            {
                loggedPositions[0] = delayedBuddy.transform.position;
                positionsToMoveTo.Add(loggedPositions[0]);
                currentlyLoggedPos++;
            }
            else if (currentlyLoggedPos == 1)
            {
                loggedPositions[1] = delayedBuddy.transform.position;
                positionsToMoveTo.Add(loggedPositions[1]);
                currentlyLoggedPos++;
            }
            else if (currentlyLoggedPos == 2)
            {
                loggedPositions[2] = delayedBuddy.transform.position;
                positionsToMoveTo.Add(loggedPositions[2]);
                currentlyLoggedPos++;
            }
            else if (currentlyLoggedPos == 3)
            {
                loggedPositions[3] = delayedBuddy.transform.position;
                positionsToMoveTo.Add(loggedPositions[3]);
                currentlyLoggedPos = 0;
            }
            timer = 0;
        }
    }*/

    public void MovePlayer()
    {

        //transform.position = Vector3.MoveTowards(transform.position, delayedBuddy.transform.position, (walkSpeed/1.5f) * Time.deltaTime);
        if (positionsToMoveTo.Count > 0)
        {
            isStationary = false;
            transform.position = Vector3.MoveTowards(transform.position, positionsToMoveTo[0], walkSpeed * Time.deltaTime);

            if (transform.position.x == positionsToMoveTo[0].x && transform.position.z == positionsToMoveTo[0].z)
            {
                positionsToMoveTo.RemoveAt(0);
            }
        }
        else
        {
            isStationary = true;
        }

        /*if (isMoving)
        {
            Quaternion targetRot;

            if (playerController.isRunning)
            {
                moveThisWay = new Vector3(delayedBuddy.transform.position.x * runSpeed, rb.velocity.y, delayedBuddy.transform.position.z * runSpeed);
            }
            else
            {
                moveThisWay = new Vector3(delayedBuddy.transform.position.x * walkSpeed, rb.velocity.y, delayedBuddy.transform.position.z * walkSpeed);
            }

            rb.velocity = moveThisWay;


            //Rotates character and DECAYS MOVEMENT
            if (analogDirection.x != 0 || analogDirection.z != 0)
            {
                angle = Mathf.Atan2(delayedBuddy.transform.position.x, delayedBuddy.transform.position.z) * Mathf.Rad2Deg;
                targetRot = Quaternion.Euler(new Vector3(0, angle, 0));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, .15f);
            }


            isStationary = false;
            if (!playerAnim.IsPlaying("WalkingAnimwithfootstep") && playerController.canJump && !playerController.isRunning)
            {
                playerAnim.Play("WalkingAnimwithfootstep");
            }

            //INSERT RUNNING ANIMATION HERE
            //else if (!playerAnim.IsPlaying("WalkingAnim") && playerController.canJump && playerController.isRunning)
            //{

            //}
        }
        if (!isMoving)
        {
            if (rb.velocity.x != 0)
            {
                if (rb.velocity.x < .3 && rb.velocity.x > -.3)
                {
                    moveThisWay = new Vector3(0, rb.velocity.y, 0);
                    rb.velocity = moveThisWay;
                    isStationary = true;
                }
                else if (rb.velocity.x > 0)
                {
                    moveThisWay = new Vector3(rb.velocity.x - moveDecayRate * Time.deltaTime * 2, rb.velocity.y, 0);
                    rb.velocity = moveThisWay;
                }
                else if (rb.velocity.x < 0)
                {
                    moveThisWay = new Vector3(rb.velocity.x + moveDecayRate * Time.deltaTime * 2, rb.velocity.y, 0);
                    rb.velocity = moveThisWay;
                }
            }
            else
            {
                isStationary = true;
            }
        }*/
    }
}
