using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScreenScript : MonoBehaviour {

    public GameObject screenDarkener;
    Renderer screenDarkenRender;
    float screenDarkenAmount = 0.74510f;

    Color darkenedColor;

    GameObject pauseMenu;

    public bool isPaused = false;
    public bool canBePaused = false;

    float originalTimeScale;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        pauseMenu = GameObject.Find("Pause");
        pauseMenu.SetActive(false);
        screenDarkenRender = screenDarkener.GetComponent<MeshRenderer>();
        darkenedColor = new Color(screenDarkenRender.material.color.r, screenDarkenRender.material.color.g, screenDarkenRender.material.color.b, screenDarkenAmount);

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            canBePaused = true;
        } else
        {
            canBePaused = false;
        }
    }

    private void Update()
    {
        if(!isPaused && canBePaused)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        darkenedColor.a = screenDarkenAmount;
        screenDarkenRender.material.color = darkenedColor;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = originalTimeScale;
        isPaused = false;
        darkenedColor.a = 0;
        screenDarkenRender.material.color = darkenedColor;
        pauseMenu.SetActive(false);
    }

}
