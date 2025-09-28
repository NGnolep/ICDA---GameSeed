using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Image redScreen;
    public Animator gameOverAnimator;
    public GameObject gameOverPanel;
    public AudioClip JumpScareSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(HandleGameOverSequence());
    }

    IEnumerator HandleGameOverSequence()
    {
        redScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        redScreen.gameObject.SetActive(false);

        gameOverAnimator.SetTrigger("JumpScare");

        audioSource.PlayOneShot(JumpScareSound);

        yield return new WaitForSeconds(3f);

        gameOverPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

