using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityKey : MonoBehaviour
{
    private bool playerInRange = false;
    public PlayerSecurityDoor playerSecurityKey;
    void Start()
    {
        
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerSecurityKey.hasAllFourKey += 1f;
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
