using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    GameObject gameManager;
    PlayerInventory playerInventory;
    //TheLightScript lightScript;

    public bool delayedMovement;
    DelayedMovementScript delayMovementScript;
    PlayerMotor playerMotor;
    public bool canJump = false;
    public bool isRunning = false;
    public GameObject landingDustPartObj;

    public bool flashlightOn = false;
    float flashlightIntensity = 7.2f;
    Quaternion flashlightDefaultRot;
    GameObject theFlashlight;
    Animation flashlightSway;

    //float rightStickRotX;
    //float rightStickRotY;

    float angleLimit = 90;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        playerInventory = gameManager.GetComponent<PlayerInventory>();
        playerMotor = GetComponent<PlayerMotor>();
        delayMovementScript = GetComponent<DelayedMovementScript>();
        theFlashlight = GameObject.Find("Flashlight");
        flashlightDefaultRot = theFlashlight.transform.rotation;
        //lightScript = GameObject.Find("TheLight").GetComponent<TheLightScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4) && isRunning == false)
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0) && canJump)
        {
            canJump = false;
            playerMotor.Jump();
        }

        //rightStickRotX = Input.GetAxisRaw("RightStickX");
        //rightStickRotY = Input.GetAxisRaw("RightStickY");

        /*
        if (Input.GetAxisRaw("RightStickX") != 0 || Input.GetAxisRaw("RightStickY") != 0)
        {
            if(transform.rotation.y > 0)
            {
                float angle = Mathf.Atan2(rightStickRotY, rightStickRotX) * Mathf.Rad2Deg;
                if (angleLimit > angle && angle > -angleLimit)
                {
                    theFlashlight.transform.localRotation = Quaternion.Euler(0, angle / 2, 0);
                }
            } else if(transform.rotation.y < 0)
            {
                float angle = Mathf.Atan2(rightStickRotX, rightStickRotY) * Mathf.Rad2Deg;
                if (-angleLimit < angle && angle < angleLimit)
                {
                    theFlashlight.transform.localRotation = Quaternion.Euler(0, angle / 2, 0);
                }
            }
        } else
        {
            theFlashlight.transform.rotation = flashlightDefaultRot;
        }*/

        if (playerInventory.hasFlashlight)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button5) && !flashlightOn)
            {
                flashlightOn = true;
            }
            else if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button5) && flashlightOn)
            {
                flashlightOn = false;
            }
        }

        if (flashlightOn)
        {
            theFlashlight.GetComponent<Light>().intensity = flashlightIntensity;
        }else
        {
            theFlashlight.GetComponent<Light>().intensity = 0;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            playerMotor.isMoving = true;
        }
        else playerMotor.isMoving = false;

        //Delayed movement
        if (delayedMovement)
        {
            delayMovementScript.MoveBuddy();
        }
        //Non delayed movement
        else
            playerMotor.ApplyMovement();


        /*if(rightStickRotX != 0)
        {
            lightScript.MoveTheLight(rightStickRotX);
        }*/
    }

    private void OnTriggerEnter(Collider triggerObj)
    {
        //RESETS JUMP WHEN COLLIDING WITH WORLD
        if (triggerObj.tag == "World")
        {
            canJump = true;
            landingDustPartObj.GetComponent<ParticleSystem>().Play();
        }

        if(triggerObj.tag == "Currency")
        {
            gameManager.GetComponent<AudioSource>().Play();

            if (triggerObj.name == "GoldGear")
            {
                if (playerInventory.currentCurrency < playerInventory.maxCurrency)
                {
                    playerInventory.CurrencyPickup(3);
                    Destroy(triggerObj.gameObject);
                }
            } else if (triggerObj.name == "SilverGear")
            {
                if (playerInventory.currentCurrency < playerInventory.maxCurrency)
                {
                    playerInventory.CurrencyPickup(2);
                    Destroy(triggerObj.gameObject);
                }
            } else if (triggerObj.name == "CopperGear")
            {
                if (playerInventory.currentCurrency < playerInventory.maxCurrency)
                {
                    playerInventory.CurrencyPickup(1);
                    Destroy(triggerObj.gameObject);
                }
            }
        }
    }
}
