using UnityEngine;
using System.Collections;

public class BossMinionSpawner : MonoBehaviour
{
    [Header("Minion Spawn Settings")]
    public GameObject minionPrefab;
    public Transform[] spawnPoints;

    [Header("Retreat Settings")]
    public Transform fallbackPoint; // Where the boss retreats to
    public float retreatSpeed = 3f;

    private bool isRetreating = false;
    private Transform bossTransform;

    private void Start()
    {
        bossTransform = transform; // Boss object this script is attached to

        // Subscribe to BossHealth threshold event
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnThresholdReached.AddListener(OnBossThresholdReached);
        }
        else
        {
            Debug.LogWarning("BossMinionSpawner: No EnemyHealth found on boss object!");
        }
    }

    private void OnBossThresholdReached()
    {
        Debug.Log("BossMinionSpawner: Threshold reached! Retreating to fallback point...");
        isRetreating = true;
    }

    private void Update()
    {
        if (isRetreating && fallbackPoint != null)
        {
            bossTransform.position = Vector2.MoveTowards(
                bossTransform.position,
                fallbackPoint.position,
                retreatSpeed * Time.deltaTime
            );

            // Check if boss reached the fallback point
            if (Vector2.Distance(bossTransform.position, fallbackPoint.position) < 0.1f)
            {
                isRetreating = false;
                StartCoroutine(SpawnMinionsAfterDelay(1f)); // Small delay before spawning
            }
        }
    }

    private IEnumerator SpawnMinionsAfterDelay(float delay)
    {
        Debug.Log("BossMinionSpawner: Boss reached fallback point. Spawning minions...");
        yield return new WaitForSeconds(delay);

        foreach (Transform point in spawnPoints)
        {
            Instantiate(minionPrefab, point.position, Quaternion.identity);
        }
    }
}

