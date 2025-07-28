using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public bool PlayerIsDeath = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ghostly"))
        {
            PlayerIsDeath = true;
            killPlayer();
        }
    }

    public void killPlayer()
    {

        SceneManager.LoadScene("Game Over");
        // Destroy(gameObject);
    }
}
