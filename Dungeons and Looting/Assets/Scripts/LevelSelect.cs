using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public Button Level1, Level2, Level3;
    public static int[] completedLevels = new int[3];
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
        SceneManager.LoadScene("fart 1");
    }
    public void SwitchScene2()
    {
        if (completedLevels[0] != 0)
        {
            SceneManager.LoadScene("fart 2");
        }
        else
        {
            print("NOT GOOD ENOUGH LOSER");
        }
        
    }
    public void SwitchScene3()
    {
        if (completedLevels[1] != 0)
        {
            SceneManager.LoadScene("fart 3");
        }
        else
        {
            print("NOT GOOD ENOUGH LOSER");
        }
        
    }
}
