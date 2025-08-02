using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainDoor : MonoBehaviour
{
    private bool canEnter = false;
    public bool hasKey = false;
    private GameObject doorInRange;

    void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.E) && hasKey)
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
        if (other.CompareTag("MainDoor") && hasKey)
        {
            canEnter = true;
            doorInRange = other.gameObject;
            Debug.Log("Can Escape");
        }
        else
        {
            Debug.Log("Need Key");
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
