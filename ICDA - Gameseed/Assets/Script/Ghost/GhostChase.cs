using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostChase : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 5f;
    public float speed = 2f;
    public float fadeDistance = 10f;

    public float roamRadius = 3f;
    public float roamTime = 2f;

    private SpriteRenderer spriteRenderer;
    private Vector2 roamTarget;
    private float roamTimer;
    public PlayerBar bar;
    private Animator animator;
    public bool See = true;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        PickNewRoamTarget();
    }

    public void Update()
    {
        Vector2 newPosition = rb.position;
        float distance = Vector2.Distance(newPosition, player.position);
        Vector2 moveDir = Vector2.zero;
        float alpha = Mathf.Clamp01(1 - (distance / fadeDistance));
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

        if (distance < chaseDistance && See)
        {
            bar.isBeingChased = true;
            moveDir = ((Vector2)player.position - newPosition).normalized;
            newPosition = rb.position + moveDir * speed * Time.deltaTime;
        }
        else
        {
            bar.isBeingChased = false;
            roamTimer -= Time.deltaTime;
            if (roamTimer <= 0f || Vector2.Distance(newPosition, roamTarget) < 0.1f)
            {
                PickNewRoamTarget();
            }

            moveDir = (roamTarget - (Vector2)newPosition).normalized;
            newPosition = rb.position + moveDir * (speed * 0.5f) * Time.deltaTime; // roam slower
        }

        rb.MovePosition(newPosition);

        if (animator != null)
        {
            animator.SetBool("isMoving", moveDir.magnitude > 0.01f);
        }
        if (spriteRenderer != null && moveDir.x != 0)
        {
            spriteRenderer.flipX = moveDir.x < 0; 
        }
    }

    public void PickNewRoamTarget()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * Random.Range(1f, roamRadius);
        roamTarget = rb.position + randomDirection;
        roamTimer = roamTime;
    }
}
