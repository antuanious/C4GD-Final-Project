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
        PlayerData.plrHp = 100;
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
        endScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerData.plrHp = 100;
    }
}
