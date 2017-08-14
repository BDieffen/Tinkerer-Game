using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLightScript : MonoBehaviour {

    GameObject player;
    PlayerInventory playerInv;

    float moveSpeed = 5f;
    public GameObject spotLight;

    public bool playerInLight;

    //float rightStickRotX;
    //float rightStickRotY;

    // Use this for initialization
    void Start () {

        player = GameObject.Find("PlayerChar");
        playerInv = GameObject.Find("GameManager").GetComponent<PlayerInventory>();
		
	}
	
	// Update is called once per frame
	void Update () {

        //rightStickRotX = Input.GetAxisRaw("RightStickX");
        //rightStickRotY = Input.GetAxisRaw("RightStickY");

    }

    private void FixedUpdate()
    {
        if (!playerInLight)
        {
            if (playerInv.charge > 0)
            {
                //playerInv.charge -= playerInv.chargeDecay * Time.deltaTime;
            }
        }else if (playerInLight)
        {
            if (playerInv.charge < playerInv.maxCharge)
            {
                playerInv.charge += playerInv.chargeDecay * Time.deltaTime * playerInv.chargeRate;
            }
            if (playerInv.charge >= playerInv.maxCharge)
            {
                playerInv.charge = playerInv.maxCharge;
            }
        }

        /*if(rightStickRotX + rightStickRotY != 0)
        {
            MoveTheLight(rightStickRotX, rightStickRotY);
        }*/
    }

    /*public void MoveTheLight(float horiDirection, float vertDirection)
    {
        //gameObject.transform.Translate(Vector3.right * horiDirection * moveSpeed * Time.deltaTime);
        //gameObject.transform.Translate(Vector3.down * vertDirection * moveSpeed * Time.deltaTime);

        Vector3 lightMov = new Vector3(horiDirection * moveSpeed, vertDirection * moveSpeed, 0);

        droneRB.velocity = lightMov;
    }*/

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerInLight = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerInLight = false;
        }
    }
}
