using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseManagerScript : MonoBehaviour {

    public TextMeshPro loadGame;
    //public TextMeshPro newGame;
    public TextMeshPro options;
    //public TextMeshPro credits;
    public TextMeshPro quitGame;

    TextMeshPro[] selections;
    GameObject[] lines;
    Vector3[] originLineSize;
    float[] lightPositions;
    [SerializeField]
    int selectionSpot;
    int previousSelection;
    //AudioSource theAudio;
    //public AudioClip selectSound;

    public GameObject selectionLights;

    float upDown;
    bool changing;

    //^^^^^^ Cut from Main Menu Script^^^^^^

    PauseScreenScript pauseScreenInitialScript;

    int[] subMenu;
    int selectedSubMenu = 0;

    // Use this for initialization
    void Start () {

        loadGame = GameObject.Find("LoadGame").GetComponent<TextMeshPro>();
        //newGame = GameObject.Find("NewGame").GetComponent<TextMeshPro>();
        options = GameObject.Find("Options").GetComponent<TextMeshPro>();
        //credits = GameObject.Find("Credits").GetComponent<TextMeshPro>();
        quitGame = GameObject.Find("Quit").GetComponent<TextMeshPro>();

        selections = new TextMeshPro[3];
        selections[0] = loadGame;
        selections[1] = options;
        selections[2] = quitGame;
        //selections[3] = credits;
        //selections[4] = quitGame;

        lines = new GameObject[3];
        lines[0] = GameObject.Find("Line1");
        lines[1] = GameObject.Find("Line3");
        lines[2] = GameObject.Find("Line5");
        //lines[3] = GameObject.Find("Line4");
        //lines[4] = GameObject.Find("Line5");

        originLineSize = new Vector3[3];
        originLineSize[0] = lines[0].transform.localScale;
        originLineSize[1] = lines[1].transform.localScale;
        originLineSize[2] = lines[2].transform.localScale;
        //originLineSize[3] = lines[3].transform.localScale;
        //originLineSize[4] = lines[4].transform.localScale;

        lightPositions = new float[3];
        lightPositions[0] = 1.42f;
        lightPositions[1] = -.44f;
        lightPositions[2] = -3f;
        //lightPositions[3] = -1.15f;
        //lightPositions[4] = -1.96f;

        for (int i = 1; i < lines.Length; i++)
        {
            lines[i].transform.localScale = new Vector3(0, 1, originLineSize[i].z);
        }

        selectionSpot = 0;
        selectionLights.transform.position = new Vector3(selectionLights.transform.position.x, lightPositions[selectionSpot], selectionLights.transform.position.z);

        previousSelection = selections.Length + 1;

        //^^^^^^ Cut from Main Menu Script^^^^^^

        pauseScreenInitialScript = GameObject.Find("GameManager").GetComponent<PauseScreenScript>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (pauseScreenInitialScript.isPaused && pauseScreenInitialScript.canBePaused)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                pauseScreenInitialScript.UnPause();
            }
        }

        MenuUpdate();
    }

    void MenuUpdate()
    {
        upDown = Input.GetAxisRaw("Vertical");
        SelectionChange();

        if (selectionLights.transform.position.y != lightPositions[selectionSpot])
        {
            selectionLights.transform.position = Vector3.Lerp(selectionLights.transform.position, new Vector3(selectionLights.transform.position.x, lightPositions[selectionSpot], selectionLights.transform.position.z), 3 * Time.unscaledDeltaTime);
        }

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == selectionSpot)
            {
                if (lines[selectionSpot].transform.localScale != originLineSize[selectionSpot])
                {
                    lines[selectionSpot].transform.localScale = Vector3.Lerp(lines[selectionSpot].transform.localScale, originLineSize[selectionSpot], 3 * Time.unscaledDeltaTime);
                }
            }
            else
            {
                if (lines[i].transform.localScale != new Vector3(0, 1, originLineSize[i].z))
                {
                    lines[i].transform.localScale = Vector3.Lerp(lines[i].transform.localScale, new Vector3(0, 1, originLineSize[i].z), 3 * Time.unscaledDeltaTime);
                }
            }
        }
    }

    void SelectionChange()
    {
        if ((upDown >= .5 || upDown <= -.5) && !changing)
        {
            changing = true;
            Change();
        }
        if (upDown == 0)
            changing = false;

        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SceneChange());
        }
    }

    void Change()
    {
        //theAudio.Play();
        previousSelection = selectionSpot;

        if (-upDown > 0)
        {
            if (selectionSpot >= 2)
            {
                selectionSpot = 0;
            }
            else selectionSpot++;
        }
        else if (-upDown < 0)
        {
            if (selectionSpot <= 0)
            {
                selectionSpot = 2;
            }
            else selectionSpot--;
        }
        //selectionLights.transform.position = new Vector3(selectionLights.transform.position.x, lightPositions[selectionSpot], selectionLights.transform.position.z);
    }

    IEnumerator SceneChange()
    {
        if (selectionSpot == 0)
        {
            float fadeTime = GetComponent<Fading>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene("Test");
        }
        else if (selectionSpot == 1)
        {
            float fadeTime = GetComponent<Fading>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene("Test");
        }
        /*else if (selectionSpot == 2)
        {
            SceneManager.LoadScene("Options");
        }*/
        /*else if (selectionSpot == 3)
        {
            SceneManager.LoadScene("Credits");
        }*/
        else
            SceneManager.LoadScene("MainMenu");
    }
}
