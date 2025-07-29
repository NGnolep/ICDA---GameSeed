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

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        PickNewRoamTarget();
    }

    public void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 moveDir = Vector2.zero;
        float alpha = Mathf.Clamp01(1 - (distance / fadeDistance));
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

        if (distance < chaseDistance)
        {
            bar.isBeingChased = true;
            moveDir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(moveDir * speed * Time.deltaTime);
        }
        else
        {
            bar.isBeingChased = false;
            roamTimer -= Time.deltaTime;
            if (roamTimer <= 0f || Vector2.Distance(transform.position, roamTarget) < 0.1f)
            {
                PickNewRoamTarget();
            }

            moveDir = (roamTarget - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(moveDir * speed * 0.5f * Time.deltaTime); // roam slower
        }

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
        roamTarget = (Vector2)transform.position + randomDirection;
        roamTimer = roamTime;
    }
}
