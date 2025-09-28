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
    public PlayerBar bar;
    private Animator animator;
    public bool See = true;
    private Rigidbody2D rb;
    public List<Waypoint> allWaypoints;
    private List<Waypoint> currentPath = new List<Waypoint>();
    private int currentPathIndex = 0;
    private Waypoint currentWaypoint;
    private bool isChasing = false;
    private float pathUpdateCooldown = 1f;
    private float pathUpdateTimer = 0f;
    private Vector2 lastPlayerPosition;
    private Vector2 lastPosition;
    private float stuckTimer = 0f;
    public float stuckCheckInterval = 1f; // how long before deciding it's stuck
    public float stuckDistanceThreshold = 0.05f;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        PickRandomRoamPath();
    }

    public void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        UpdateFade(distance);

        if (distance < chaseDistance && See && bar.currentSanity < 40f )
        {
            if (!isChasing)
            {
                isChasing = true;
            }

            bar.isBeingChased = true;

            // Chase directly
            MoveDirectlyToPlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                PickRandomRoamPath(); // Resume roaming
            }

            bar.isBeingChased = false;
            MoveAlongPath(); // Roaming
        }
        CheckIfStuck();
    }
    void MoveDirectlyToPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        Vector2 nextPosition = rb.position + direction * speed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 0.2f, LayerMask.GetMask("Wall"));
        if (hit.collider == null)
        {
            rb.MovePosition(nextPosition);
        }

        if (animator != null)
            animator.SetBool("isMoving", direction.magnitude > 0.01f);
        if (spriteRenderer != null && direction.x != 0)
            spriteRenderer.flipX = direction.x < 0;
    }

    void CheckIfStuck()
    {
        float movedDistance = Vector2.Distance(transform.position, lastPosition);

        if (movedDistance < stuckDistanceThreshold)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckCheckInterval)
            {
                stuckTimer = 0f;
                ForcePickNeighborWaypoint();
            }
        }
        else
        {
            stuckTimer = 0f;
            lastPosition = transform.position;
        }
    }

    void ForcePickNeighborWaypoint()
    {
        Waypoint current = FindClosestWaypoint(transform.position);
        if (current != null && current.neighbors.Count > 0)
        {
            Waypoint randomNeighbor = current.neighbors[Random.Range(0, current.neighbors.Count)];
            currentPath = new List<Waypoint> { randomNeighbor };
            currentPathIndex = 0;
            Debug.Log("Ghost stuck — picking neighbor waypoint to escape.");
        }
    }
    void UpdateFade(float distance)
    {
        float alpha = Mathf.Clamp01(1 - (distance / fadeDistance));
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    void MoveAlongPath()
    {
        if (currentPath == null || currentPath.Count == 0 || currentPathIndex >= currentPath.Count)
            return;

        Vector2 target = currentPath[currentPathIndex].transform.position;
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        Vector2 nextPosition = rb.position + direction * speed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 0.05f, LayerMask.GetMask("Wall"));
        if (hit.collider == null)
        {
            rb.MovePosition(nextPosition);
        }
        else
        {
            pathUpdateTimer = 0f; 
            return;
        }

        if (animator != null)
            animator.SetBool("isMoving", direction.magnitude > 0.01f);
        if (spriteRenderer != null && direction.x != 0)
            spriteRenderer.flipX = direction.x < 0;

        if (Vector2.Distance(transform.position, target) < 0.1f)
            currentPathIndex++;
    }

    void UpdatePathToPlayer()
    {
        Waypoint start = FindClosestWaypoint(transform.position);
        Waypoint end = FindClosestWaypoint(player.position);
        currentPath = AStarPathFinder.FindPath(start, end);
        currentPathIndex = 0;
    }

    void PickRandomRoamPath()
    {
        Waypoint start = FindClosestWaypoint(transform.position);
        Waypoint end = null;
        int attempts = 10;

        while (attempts-- > 0)
        {
            end = allWaypoints[Random.Range(0, allWaypoints.Count)];
            if (Vector2.Distance(start.transform.position, end.transform.position) > 1f) break;
        }

        currentPath = AStarPathFinder.FindPath(start, end);
        currentPathIndex = 0;
    }

    Waypoint FindClosestWaypoint(Vector2 position)
    {
        float minDist = float.MaxValue;
        Waypoint closest = null;
        foreach (Waypoint wp in allWaypoints)
        {
            float dist = Vector2.Distance(position, wp.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = wp;
            }
        }
        return closest;
    }
}
