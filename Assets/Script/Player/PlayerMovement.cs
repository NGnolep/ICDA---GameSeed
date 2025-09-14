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
    public PlayerHiding hid;
    private Animator animator;
    private Coroutine regenCoroutine;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hid = GetComponent<PlayerHiding>();
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
        if (hid.hiding == false)
        {
            Vector2 finalVelocity = new Vector2(movement.x * horizontalMultiplier, movement.y);
            rb.velocity = finalVelocity * currentSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    
}

