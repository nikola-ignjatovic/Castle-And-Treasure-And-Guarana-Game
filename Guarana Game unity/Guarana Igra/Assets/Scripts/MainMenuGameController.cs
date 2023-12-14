using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuGameController : MonoBehaviour
{
    public Button Exit;
    public Button PlayGame;
    // Start is called before the first frame update
    void Start()
    {
        Exit.onClick.AddListener(() => FExitGame());
        PlayGame.onClick.AddListener(() => FPlayGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FExitGame()
    {
        Application.Quit();
    }
    public void FPlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
