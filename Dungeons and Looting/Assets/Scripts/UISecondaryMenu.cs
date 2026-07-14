using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISecondaryMenu : MonoBehaviour
{
    public Button levelButton;

    public void OpenLevelButton()
    {
        SceneManager.LoadScene("Levels_Menu");
    }
    // Start is called before the first frame update
    void Start()
    {
        levelButton.onClick.AddListener(OpenLevelButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
