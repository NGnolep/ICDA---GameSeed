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

    void Start()
    {
        StartCoroutine(HandleGameOverSequence());
    }

    IEnumerator HandleGameOverSequence()
    {
        redScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        redScreen.gameObject.SetActive(false);

        gameOverAnimator.SetTrigger("JumpScare");

        yield return new WaitForSeconds(0.5f);

        gameOverPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

