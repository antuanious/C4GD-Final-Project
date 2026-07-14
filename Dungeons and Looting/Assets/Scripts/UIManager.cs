using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button playButton;

    public void StartGameButton()
    {
        SceneManager.LoadScene("Secondary_Menu");
    }
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(StartGameButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
