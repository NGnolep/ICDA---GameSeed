using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float currentSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    public PlayerBar bar;
    private Coroutine regenCoroutine;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = movement * currentSpeed;
    }
}
