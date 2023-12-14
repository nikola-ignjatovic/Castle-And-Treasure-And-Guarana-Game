using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class VictoryScreenGameControllerScript : MonoBehaviour
{
    public Button Exit;
    public Button MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        Exit.onClick.AddListener(() => FExitGame());
        MainMenu.onClick.AddListener(() => FMainMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void FExitGame()
    {
        Application.Quit();
    }
}
