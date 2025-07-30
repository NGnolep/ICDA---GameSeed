using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float sprintSpeed = 3f;
    public float currentSpeed;
    public float horizontalMultiplier = 1.5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public PlayerBar bar;
    private Animator animator;
    private Coroutine regenCoroutine;
    private SpriteRenderer rend;
    private bool canHide = false;
    private bool hiding = false;
    public GhostChase GhostScript;
    public bool NowHide = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && bar != null && bar.currentStamina > 0)
        {
            currentSpeed = sprintSpeed;
            bar.UseStamina(10f * Time.deltaTime);
            //Debug.Log(bar.currentStamina);
        }
        else
        {
            currentSpeed = moveSpeed;
            //Debug.Log(bar.currentStamina);
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (Input.GetKeyDown(KeyCode.E) && canHide && !NowHide)
        {
            NowHide = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && NowHide)
        {
            NowHide = false;
            rend.renderingLayerMask = 2;
            hiding = false;
            Debug.Log("not hiding");
            GhostScript.See = true;
        }

        if (NowHide)
        {
            rend.renderingLayerMask = 0;
            hiding = true;
            GhostScript.See = false;
            Debug.Log("Hiding now");
        }
        
        

        
        
        
            
        
        //rb.velocity = movement * currentSpeed;



        if (animator != null)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
            animator.SetBool("isMoving", movement.magnitude > 0.01f);

        }


    }

    public void FixedUpdate()
    {
        if (!hiding)
        {
            Vector2 finalVelocity = new Vector2(movement.x * horizontalMultiplier, movement.y);
            rb.velocity = finalVelocity * currentSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TheLocker"))
        {
            canHide = true;
            Debug.Log("Can Hide");
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

