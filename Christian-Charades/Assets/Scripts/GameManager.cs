using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelsPanel;
    public GameObject infoPanel;
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }



    public void LoadLevel()
    {
        SceneManager.LoadScene("Characters");
        print("Characters");

    }


}
