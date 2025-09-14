using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDOORS : MonoBehaviour
{
    private bool canEnter = false;
    private GameObject doorInRange;

    void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.E))
        {
            if (doorInRange != null)
            {
                doorInRange.GetComponent<SpriteRenderer>().enabled = false;
                doorInRange.GetComponent<BoxCollider2D>().enabled = false;
                Debug.Log("Door Opened");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TheDoor"))
        {
            canEnter = true;
            doorInRange = other.gameObject;
            Debug.Log("Can Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TheDoor"))
        {
            other.GetComponent<SpriteRenderer>().enabled = true;
            other.GetComponent<BoxCollider2D>().enabled = true;
            canEnter = false;
            doorInRange = null;
            Debug.Log("Door Closed");
        }
    }
}
