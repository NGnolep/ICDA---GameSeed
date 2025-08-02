using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroTextManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject panel2;
    public TextMeshProUGUI introText;
    public Image blackImage;
    public float typingSpeed = 0.05f;
    public string[] lines;
    public float fadeSpeed = 1f;

    private int currentLine = 0;

    void Start()
    {
        PlayerPrefs.DeleteKey("HasPlayedIntro");
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("HasPlayedIntro", 0) == 1)
        {
            panel.SetActive(false);
            return;
        }
        Time.timeScale = 0f;
        panel.SetActive(true);
        blackImage.color = new Color(0, 0, 0, 1); // full black
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        string rawLine = lines[currentLine];
        Color targetColor = Color.white;

        if (rawLine.Contains("[red]"))
        {
            targetColor = Color.red;
            rawLine = rawLine.Replace("[red]", "");
        }

        introText.text = ""; 
        introText.text += "<color=#" + ColorUtility.ToHtmlStringRGB(targetColor) + ">";


        foreach (char letter in rawLine.ToCharArray())
        {
            introText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        introText.text += "</color>\n"; // end color and add newline

        yield return new WaitForSecondsRealtime(1f);
        currentLine++;

        if (currentLine < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            PlayerPrefs.SetInt("HasPlayedIntro", 1);
            PlayerPrefs.Save();
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.unscaledDeltaTime * fadeSpeed;
            blackImage.color = new Color(0, 0, 0, alpha);
            introText.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        panel.SetActive(false);
        Time.timeScale = 1f;
        panel2.SetActive(true);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
