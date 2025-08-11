using UnityEngine;

public class WallActivator : MonoBehaviour
{
    public GameObject wall; // Assign in Inspector
    public GameObject BossSpawn; // Assign your boss spawner GameObject here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the wall
            wall.SetActive(true);

            // Activate the boss spawner
            if (BossSpawn != null)
            {
                BossSpawn.SetActive(true);
                Debug.Log("Boss spawner enabled!");
            }

            Debug.Log("Trigger activated by: " + other.name);
        }
    }
}
