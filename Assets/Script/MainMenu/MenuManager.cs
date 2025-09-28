using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public AudioClip ClickSound;
    public AudioClip hoverClip;

    private AudioSource audioSource;

    private void Start()
    {
        // Load saved volume or use default
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        audioSource.PlayOneShot(ClickSound);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        audioSource.PlayOneShot(ClickSound);
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        audioSource.PlayOneShot(ClickSound);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        audioSource.PlayOneShot(ClickSound);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverClip);
        }
    }
}
