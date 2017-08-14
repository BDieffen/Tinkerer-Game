using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour {

    public GameObject fins;
    //Vector3 currentRotation;
    Vector3 rotateTo;
    float rotationSpeed = 10f;

    float moveSpeed = 15f;
    float rightStickRotX;
    float rightStickRotY;
    Vector3 droneMoveThisWay;
    Rigidbody droneRB;
    float moveDecayRate = 2f;

    bool isMoving = false;

    // Use this for initialization
    void Start () {
        droneRB = GetComponent<Rigidbody>();		
	}
	
	// Update is called once per frame
	void Update () {

        //currentRotation = fins.transform.eulerAngles;
        rotateTo = new Vector3(0, 0, 10);

        fins.transform.Rotate(rotateTo);

        rightStickRotX = Input.GetAxisRaw("RightStickX");
        rightStickRotY = Input.GetAxisRaw("RightStickY");
    }

    private void FixedUpdate()
    {
        if (rightStickRotX + rightStickRotY != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        DroneMovement(rightStickRotX, rightStickRotY);
    }

    public void DroneMovement(float horiDirection, float vertDirection)
    {
        if (isMoving)
        {
            droneMoveThisWay = new Vector3(horiDirection * moveSpeed, -vertDirection * moveSpeed, 0);

            droneRB.AddForce(droneMoveThisWay);
        }
    }
}
