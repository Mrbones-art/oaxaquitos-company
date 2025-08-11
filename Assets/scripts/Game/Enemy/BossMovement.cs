using UnityEngine;

public class BossMovement : MonoBehaviour
{
       public GameObject player;
    public float speed;
    public float detectionRange = 4f;
    public LayerMask obstacleLayers; // Layers that count as walls/obstacles
    public float collisionCheckRadius = 0.2f; // Size of the circle to check for obstacles

    private float distance;
    private Vector3 lastKnownPosition;
    private bool playerSeen;

    private void Awake()
    {
        // Auto-assign the player if not set
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj;
            else
                Debug.LogWarning("EnemyMovement: No Player found in scene with tag 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < detectionRange)
        {
            playerSeen = true;
            lastKnownPosition = player.transform.position;

            MoveTowards(player.transform.position);
        }
        else if (playerSeen)
        {
            float lastKnownDistance = Vector2.Distance(transform.position, lastKnownPosition);

            if (lastKnownDistance > 0.1f)
            {
                MoveTowards(lastKnownPosition);
            }
            else
            {
                playerSeen = false;
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Check if there's a wall in the way
        bool blocked = Physics2D.CircleCast(
            transform.position, 
            collisionCheckRadius, 
            direction, 
            speed * Time.deltaTime, 
            obstacleLayers
        );

        if (!blocked)
        {
            // Move only if path is clear
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw collision check radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius);

        // Draw line to last known position
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, lastKnownPosition);
        Gizmos.DrawSphere(lastKnownPosition, 0.1f);
    }
}

