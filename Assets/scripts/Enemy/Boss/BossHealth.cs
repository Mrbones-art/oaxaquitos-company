using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour
{
       public int maxHealth = 100;
    private int currentHealth;

    public int thresholdHealth = 50; // When to trigger the event
    private bool thresholdTriggered = false;

    public UnityEvent<int> OnHealthChanged; // Passes current health
    public UnityEvent OnThresholdReached;
    public UnityEvent OnDeath;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {

        currentHealth -= amount;
        if (!thresholdTriggered && currentHealth <= thresholdHealth)
        {
            thresholdTriggered = true;
            OnThresholdReached?.Invoke();
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

