using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseSceneGameController : MonoBehaviour
{
    public Button PlayGameButton;
    public Button MainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        PlayGameButton.onClick.AddListener(PlayGameFunction);
        MainMenuButton.onClick.AddListener(MainMenuFunction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayGameFunction()
    {
        SceneManager.LoadScene("InGameScene");
    }
    void MainMenuFunction()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
