using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicWizard;
    void Awake()
    {
        musicWizard = this.gameObject.GetComponent<AudioSource>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("Music")==1)
        {
            StartPlayingMusic();
        }
        else
        {
            StopPlayingMusic();
        }
    }
    private void StartPlayingMusic()
    {
        if(musicWizard.isPlaying!=true)
        {
            musicWizard.Play();
        }
        else
        {

        }
    }
    private void StopPlayingMusic()
    {
        if (musicWizard.isPlaying == true)
        {
            musicWizard.Stop();
        }
        else
        {

        }
    }
}
