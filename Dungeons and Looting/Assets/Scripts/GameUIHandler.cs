using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameUIHandler instance;
    public GameObject endScreen;
    public Button mainMenuButton, respawnButton;

    void Start()
    {
        instance = this;
        endScreen.SetActive(false);
        mainMenuButton.onClick.AddListener(MainMenuReturn);
        respawnButton.onClick.AddListener(RestartScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableEndScreen()
    {
        endScreen.SetActive(true);
    }

    public void MainMenuReturn()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
