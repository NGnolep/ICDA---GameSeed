using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public AudioClip ClickSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Resume()
    {
        audioSource.PlayOneShot(ClickSound);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        //audioSource.PlayOneShot(ClickSound);
        pauseMenu.SetActive(true);
        
        Time.timeScale = 0;
    }

    public void Home()
    {
        audioSource.PlayOneShot(ClickSound);
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

}
