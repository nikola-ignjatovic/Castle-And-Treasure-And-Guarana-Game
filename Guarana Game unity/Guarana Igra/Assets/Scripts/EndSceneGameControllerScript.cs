using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneGameControllerScript : MonoBehaviour
{
    public Button RestartGame;
    public Button RestartGame2;
    // Start is called before the first frame update
    void Start()
    {
        RestartGame.onClick.AddListener(()=> FRestartGame());
        RestartGame2.onClick.AddListener(() => FRestartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FRestartGame()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetInt("Health", 3);
    }
}
