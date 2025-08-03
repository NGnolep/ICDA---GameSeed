using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityKey : MonoBehaviour
{
    private bool playerInRange = false;
    public PlayerSecurityDoor playerSecurityKey;
    public AudioClip SecureKeySound;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerSecurityKey.hasAllFourKey += 1f;
            audioSource.PlayOneShot(SecureKeySound);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerSecurityKey = collision.GetComponent<PlayerSecurityDoor>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerSecurityKey = null;
        }
    }
}
