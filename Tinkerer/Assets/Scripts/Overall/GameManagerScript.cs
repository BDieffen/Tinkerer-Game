using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    string filePath;
    int playerCurrency;
    int playerEXP;

    string currentScene;
    string lastScene;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentScene = SceneManager.GetActiveScene().name;
        if(lastScene == null)
        {
            lastScene = currentScene;
        }
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update () {

	}

    void SceneChange()
    {

    }
}
