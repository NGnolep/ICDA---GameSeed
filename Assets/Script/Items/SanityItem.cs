using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityItem : MonoBehaviour
{
    public float sanityRestoreAmount = 20f;
    private bool playerInRange = false;
    private PlayerBar playerBar;
    public SanitySpawner SaneSpawn;

    public AudioClip pillSound;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerBar.RegainSanity(sanityRestoreAmount);
            SaneSpawn.recharging();
            audioSource.PlayOneShot(pillSound);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerBar = collision.GetComponent<PlayerBar>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerBar = null;
        }
    }
}
