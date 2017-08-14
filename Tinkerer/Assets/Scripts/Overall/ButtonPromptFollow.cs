using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPromptFollow : MonoBehaviour {

    GameObject target;
    Vector3 targetPos;
    GameObject[] prompts = new GameObject[4];

	// Use this for initialization
	void Start () {
        target = GameObject.Find("PlayerChar");
        prompts[0] = GameObject.Find("APrompt");
        prompts[1] = GameObject.Find("BPrompt");
        prompts[2] = GameObject.Find("XPrompt");
        prompts[3] = GameObject.Find("YPrompt");
    }
	
	// Update is called once per frame
	void Update () {

        targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x - 2.2f, targetPos.y + 1.5f, transform.position.z);
		
	}

    public void RegainTarget()
    {
        target = GameObject.Find("PlayerChar");
    }


    public void ShowPrompt(int button)
    {
        switch (button)
        {
            case 1:
                prompts[0].GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2:
                prompts[1].GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 3:
                prompts[2].GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 4:
                prompts[3].GetComponent<SpriteRenderer>().enabled = true;
                break;
            default:
                break;
        }
    }

    public void HidePrompt(int button)
    {
        switch (button)
        {
            case 1:
                prompts[0].GetComponent<SpriteRenderer>().enabled = false;
                break;
            case 2:
                prompts[1].GetComponent<SpriteRenderer>().enabled = false;
                break;
            case 3:
                prompts[2].GetComponent<SpriteRenderer>().enabled = false;
                break;
            case 4:
                prompts[3].GetComponent<SpriteRenderer>().enabled = false;
                break;
            default:
                break;
        }
    }
}
