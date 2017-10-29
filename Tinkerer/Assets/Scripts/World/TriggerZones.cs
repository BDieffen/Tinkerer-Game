using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZones : MonoBehaviour {

    //public List<GameObject> thingsToPlace = new List<GameObject>();

    bool playerInTrigger = false;

    public GameObject thingToPlace;

    bool canActivate = false;

    public float leverTarget;
    public GameObject affectedObj;
    public Vector3 startingLoc;
    public Vector3 targetLoc;
    public float singleAxisTarget;

    bool canMove = false;

    ButtonPromptFollow buttonPrompts;

    public List<GameObject> messages = new List<GameObject>();
    public List<GameObject> messageLights = new List<GameObject>();

	// Use this for initialization
	void Start () {
        buttonPrompts = GameObject.Find("ButtonPrompts").GetComponent<ButtonPromptFollow>();

    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            LeverTrigger();
        }

        if (canMove && affectedObj.transform.position.y >= singleAxisTarget)
        {
            if(thingToPlace.transform.position.y > leverTarget)
            {
                thingToPlace.transform.Translate(thingToPlace.transform.up * Time.deltaTime * 2);
            }
            affectedObj.transform.Translate(Vector3.down * Time.deltaTime);
        }
    }

    public void LeverTrigger()
    {
        if (canActivate && thingToPlace.GetComponent<MeshRenderer>().enabled == true)
        {
            canMove = true;
            buttonPrompts.HidePrompt(3);
        }
        if (!canActivate)
        {
            if (GameObject.Find("GameManager").GetComponent<PlayerInventory>().SearchInventory("Lever"))
            {
                GameObject.Find("GameManager").GetComponent<PlayerInventory>().RemoveItemFromList("Lever");
                ParticleSystem lever1Part = GameObject.Find("Lever1Part").GetComponent<ParticleSystem>();
                lever1Part.Play();
                thingToPlace.GetComponent<MeshRenderer>().enabled = true;

                canActivate = true;
            }
            else
            {
                messages[0].GetComponent<MeshRenderer>().sortingOrder = 1;
                messages[0].GetComponent<MeshRenderer>().enabled = true;
                messageLights[0].GetComponent<Light>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag.Contains("Player") && !canMove)
        {
            playerInTrigger = true;
            buttonPrompts.ShowPrompt(3);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag.Contains("Player"))
        {
            playerInTrigger = false;
            buttonPrompts.HidePrompt(3);
            messages[0].GetComponent<MeshRenderer>().enabled = false;
            messageLights[0].GetComponent<Light>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (buttonPrompts != null)
        {
            buttonPrompts.HidePrompt(1);
            buttonPrompts.HidePrompt(2);
            buttonPrompts.HidePrompt(3);
            buttonPrompts.HidePrompt(4);
        }
    }
}
