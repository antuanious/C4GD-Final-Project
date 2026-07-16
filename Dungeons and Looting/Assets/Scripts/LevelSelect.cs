using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public Button Level1, Level2, Level3;
    // Start is called before the first frame update
    void Start()
    {
        Level1.onClick.AddListener(SwitchScene1);
        Level2.onClick.AddListener(SwitchScene2);
        Level3.onClick.AddListener(SwitchScene3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene1()
    {
        SceneManager.LoadScene("fart");
    }
    public void SwitchScene2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void SwitchScene3()
    {
        SceneManager.LoadScene("Level 3");
    }
}
