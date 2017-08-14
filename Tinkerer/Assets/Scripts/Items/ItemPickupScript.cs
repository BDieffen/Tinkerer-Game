using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour {

    bool playerInTrigger = false;
    PlayerInventory inventory;
    ButtonPromptFollow buttonPrompts;

	// Use this for initialization
	void Start () {

        buttonPrompts = GameObject.Find("ButtonPrompts").GetComponent<ButtonPromptFollow>();
        inventory = GameObject.Find("GameManager").GetComponent<PlayerInventory>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (playerInTrigger && Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if (!inventory.SearchInventory(name))
            {
                inventory.CreateItem(name, 1);
                Destroy(gameObject);
            }
        }

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Player"))
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
        }
    }

    private void OnDestroy()
    {
        buttonPrompts.HidePrompt(1);
        buttonPrompts.HidePrompt(2);
        buttonPrompts.HidePrompt(3);
        buttonPrompts.HidePrompt(4);
    }
}
