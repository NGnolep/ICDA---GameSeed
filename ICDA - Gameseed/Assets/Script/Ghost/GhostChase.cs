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
    public List<Waypoint> allWaypoints;
    private List<Waypoint> currentPath = new List<Waypoint>();
    private int currentPathIndex = 0;
    private Waypoint currentWaypoint;
    private bool isChasing = false;
    private float pathUpdateCooldown = 1f;
    private float pathUpdateTimer = 0f;

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

        if (distance < chaseDistance && See)
        {
            if (!isChasing)
            {
                isChasing = true;
                pathUpdateTimer = 0f;
            }

            bar.isBeingChased = true;

            pathUpdateTimer -= Time.deltaTime;
            if (pathUpdateTimer <= 0f)
            {
                pathUpdateTimer = pathUpdateCooldown;
                UpdatePathToPlayer();
            }
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                PickRandomRoamPath();
            }

            bar.isBeingChased = false;
        }
        MoveAlongPath();
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
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

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
        currentPath = FindPath(start, end);
        currentPathIndex = 0;
    }

    void PickRandomRoamPath()
    {
        Waypoint start = FindClosestWaypoint(transform.position);
        Waypoint end = allWaypoints[Random.Range(0, allWaypoints.Count)];
        currentPath = FindPath(start, end);
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

    List<Waypoint> FindPath(Waypoint start, Waypoint goal)
    {
        List<Waypoint> openSet = new List<Waypoint> { start };
        Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
        Dictionary<Waypoint, float> gScore = new Dictionary<Waypoint, float>();
        Dictionary<Waypoint, float> fScore = new Dictionary<Waypoint, float>();

        foreach (Waypoint wp in allWaypoints)
        {
            gScore[wp] = float.MaxValue;
            fScore[wp] = float.MaxValue;
        }

        gScore[start] = 0;
        fScore[start] = Vector2.Distance(start.transform.position, goal.transform.position);

        while (openSet.Count > 0)
        {
            Waypoint current = openSet[0];
            foreach (Waypoint wp in openSet)
            {
                if (fScore[wp] < fScore[current])
                    current = wp;
            }

            if (current == goal)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);

            foreach (Waypoint neighbor in current.neighbors)
            {
                float tentativeG = gScore[current] + Vector2.Distance(current.transform.position, neighbor.transform.position);

                if (tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Vector2.Distance(neighbor.transform.position, goal.transform.position);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return new List<Waypoint>(); // No path found
    }

    List<Waypoint> ReconstructPath(Dictionary<Waypoint, Waypoint> cameFrom, Waypoint current)
    {
        List<Waypoint> totalPath = new List<Waypoint> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }
        return totalPath;
    }
}
