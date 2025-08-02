using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKey : MonoBehaviour
{
    private bool playerInRange = false;
    public PlayerMainDoor playerMainKey;
    void Start()
    {
        
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMainKey.hasKey = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerMainKey = collision.GetComponent<PlayerMainDoor>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerMainKey = null;
        }
    }
}
