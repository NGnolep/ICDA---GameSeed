using System.Collections;
using System.Collections.Generic;
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

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        PickNewRoamTarget();
    }

    public void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        float alpha = Mathf.Clamp01(1 - (distance / fadeDistance));
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

        if (distance < chaseDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        else
        {
            roamTimer -= Time.deltaTime;
            if (roamTimer <= 0f || Vector2.Distance(transform.position, roamTarget) < 0.1f)
            {
                PickNewRoamTarget();
            }

            Vector2 direction = (roamTarget - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * speed * 0.5f * Time.deltaTime); // roam slower
        }
    }

    public void PickNewRoamTarget()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * Random.Range(1f, roamRadius);
        roamTarget = (Vector2)transform.position + randomDirection;
        roamTimer = roamTime;
    }
}
