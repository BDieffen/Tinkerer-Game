using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    Animation playerAnim;
    [SerializeField]
    float currentSpeed;
    float runSpeed = 5;
    float walkSpeed = 2;
    private float moveDecayRate = 15;
    float jumpHeight = 12;
    public bool isMoving = false;
    [SerializeField]
    bool isStationary = true;
    Vector3 moveThisWay;
    Rigidbody rb;
    PlayerController playerController;
    public Vector3 analogDirection;
    float angle;
    //Animation footStepSounds;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerAnim = GetComponent<Animation>();
        //footStepSounds = GameObject.Find("FootstepAudio").GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        analogDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (playerController.delayedMovement || (isStationary && !isMoving && !playerAnim.IsPlaying("Idle")))
        {
            playerAnim.Play("Idle");
        }
        else if(isMoving) {
            playerAnim.Stop("Idle");
        }
    }

    private void FixedUpdate()
    {
        currentSpeed = rb.velocity.x;
    }

    public void ApplyMovement()
    {
        if (isMoving)
        {
            Quaternion targetRot;

            if (playerController.isRunning)
            {
                moveThisWay = new Vector3(Input.GetAxisRaw("Horizontal") * runSpeed, rb.velocity.y, (Input.GetAxisRaw("Vertical") * runSpeed));
            }
            else
            {
                moveThisWay = new Vector3(Input.GetAxisRaw("Horizontal") * walkSpeed, rb.velocity.y, Input.GetAxisRaw("Vertical") * walkSpeed);
            }

            rb.velocity = moveThisWay;

            //ORIGINAL CODE!!!!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Rotates character and DECAYS MOVEMENT
            /*if (rb.velocity.x < 0 && transform.rotation.y != -90)
            {
                targetRot = Quaternion.Euler(0, -90, 0);
                transform.rotation = targetRot;
            }
            else if(rb.velocity.x > 0 && transform.rotation.y != 90)
            {
                targetRot = Quaternion.Euler(0, 90, 0);
                transform.rotation = targetRot;
            }*/
            //END OF ORIGINAL CODE!!!!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            
            //Rotates character and DECAYS MOVEMENT
            if (analogDirection.x != 0 || analogDirection.z != 0)
            {
                angle = Mathf.Atan2(analogDirection.x, analogDirection.z) * Mathf.Rad2Deg;
                targetRot = Quaternion.Euler(new Vector3(0, angle, 0));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, .15f);
            }


            isStationary = false;
            if (!playerAnim.IsPlaying("WalkingAnimwithfootstep") && playerController.canJump && !playerController.isRunning)
            {
                playerAnim.Play("WalkingAnimwithfootstep");
            }

            //INSERT RUNNING ANIMATION HERE
            /*else if (!playerAnim.IsPlaying("WalkingAnim") && playerController.canJump && playerController.isRunning)
            {

            }*/
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
        }
    }

    public void Jump()
    {
        if (playerAnim.IsPlaying("WalkingAnimwithfootstep"))
        {
            playerAnim.Stop("WalkingAnimwithfootstep");
        }
        rb.velocity += Vector3.up * jumpHeight;
    }
}
