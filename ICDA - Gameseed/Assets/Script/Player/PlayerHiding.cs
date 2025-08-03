using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private Collider2D coll;
    private SpriteRenderer rend;
    private bool canHide = false;
    public bool hiding = false;
    public GhostChase GhostScript;
    public bool NowHide = false;
    public AudioClip CabinetSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canHide && !NowHide)
        {
            NowHide = true;
            coll.enabled = false;
            audioSource.PlayOneShot(CabinetSound);
        }
        else if (Input.GetKeyDown(KeyCode.E) && NowHide)
        {
            NowHide = false;
            rend.enabled = true;
            hiding = false;
            //Debug.Log("not hiding");
            GhostScript.See = true;
            coll.enabled = true;
            audioSource.PlayOneShot(CabinetSound);
        }

        if (NowHide)
        {
            rend.enabled = false;
            hiding = true;
            GhostScript.See = false;
            //Debug.Log("Hiding now");
        }
    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TheLocker"))
        {
            canHide = true;
            //Debug.Log("Can Hide");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TheLocker"))
        {
            canHide = false;
        }
    }
}
