using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuGameControllerScript : MonoBehaviour
{
    public Button PlayGameButton;
    public Button ExitButton;
    public Button HardButton;
    public Button EasyButton;
    public Button MusicButton;
    public Button WeatherButton;

    public TMP_Text HardButtonText;
    public TMP_Text EasyButtonText;
    public TMP_Text MusicButtonText;
    public TMP_Text WeatherButtonText;

    void Start()
    {
        PlayGameButton.onClick.AddListener(PlayGameFunction);
        ExitButton.onClick.AddListener(ExitFunction);
        HardButton.onClick.AddListener(HardFunction);
        EasyButton.onClick.AddListener(EasyFunction);
        MusicButton.onClick.AddListener(MusicFunction);
        WeatherButton.onClick.AddListener(WeatherFunction);

        if (PlayerPrefs.GetInt("Difficulty")!=1 && PlayerPrefs.GetInt("Difficulty") != 2) // Setting difficulty to easy by default
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        if (PlayerPrefs.GetInt("Music")!=1 && PlayerPrefs.GetInt("Music") != 0) // Setting music to on by default
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        if (PlayerPrefs.GetInt("Weather") != 1 && PlayerPrefs.GetInt("Weather") != 0) // Setting music to on by default
        {
            PlayerPrefs.SetInt("Weather", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Difficulty") == 1) // Giving Sign that easy difficulty is selected
        {
            EasyButtonText.text = "► Easy";
            HardButtonText.text = "Hard";
        }
        if (PlayerPrefs.GetInt("Difficulty") == 2) // Giving Sign that hard difficulty is selected
        {
            EasyButtonText.text = "Easy";
            HardButtonText.text = "► Hard";
        }
        if (PlayerPrefs.GetInt("Music") == 1) // Giving Sign that music is Activated/Disactivated
        {
            MusicButtonText.text = "► Music";
        }
        else
        {
            MusicButtonText.text = "Music";
        }
        if (PlayerPrefs.GetInt("Weather") == 1) // Giving Sign that music is Activated/Disactivated
        {
            WeatherButtonText.text = "► Weather";
        }
        else
        {
            WeatherButtonText.text = "Weather";
        }
    }
    void PlayGameFunction()
    {
        SceneManager.LoadScene("InGameScene");
    }
    void ExitFunction()
    {
        Application.Quit();
    }
    void HardFunction()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
    }
    void EasyFunction()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
    }
    void MusicFunction() // Music Activation/Disactivation
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    void WeatherFunction() // Weather Activation/Disactivation
    {
        if (PlayerPrefs.GetInt("Weather") == 1)
        {
            PlayerPrefs.SetInt("Weather", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Weather", 1);
        }
    }
}
