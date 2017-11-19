using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBuddyScript : MonoBehaviour {

    [SerializeField]
    bool playerCanMove = false;
    float delayLength = 2f;
    float timer = 0;
    public GameObject delayedBuddy;
    Rigidbody buddyRB;
    public DelayedMovementScript delayedMoveScript;
    Vector3[] loggedPositions = new Vector3[4];
    int currentlyLoggedPos = 0;
    [SerializeField]
    List<Vector3> positionsToMoveTo = new List<Vector3>();

    public Vector3 analogDirection;
    float currentSpeed;
    float runSpeed = 5;
    float walkSpeed = 2;
    PlayerController playerController;
    PlayerMotor playerMotor;

    // Use this for initialization
    void Start()
    {
        buddyRB = delayedBuddy.GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerMotor = GetComponent<PlayerMotor>();
        //footStepSounds = GameObject.Find("FootstepAudio").GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        analogDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        currentSpeed = buddyRB.velocity.x;
        if (playerCanMove)
        {
            delayedMoveScript.MovePlayer();
        }

        //Make this looser. As of right now the player has to be exactly on top of delayBuddy for this to not trigger. Make a dead zone where the player is close enough to the delayBuddy for this to not trigger
        if (transform.position != delayedBuddy.transform.position && !playerCanMove)
        {
            DelayCountdown();
        }
    }

    //THIS SHOULD START THE COUNTDOWN TIL THE PLAYER STARTS MOVING TOWARDS THE delayBuddy OBJECT. DOES NOT WORK RIGHT NOW. MUST FIX
    public void DelayCountdown()
    {
        /*for(float i = 0; i < delayLength; i += Time.deltaTime)
        {

        }*/
        if (loggedPositions != null)
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
    }
}
