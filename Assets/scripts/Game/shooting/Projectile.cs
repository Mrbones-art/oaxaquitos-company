using UnityEngine;

namespace TopDown.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("Movement Stats")]
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;
        [SerializeField] private int damage = 25; // Damage dealt by bullet

        private Rigidbody2D body;
        private float lifetimer;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        public void ShootBullet(Transform shootPoint)
        {
            lifetimer = 0;
            body.linearVelocity = Vector2.zero;
            transform.position = shootPoint.position;
            transform.rotation = shootPoint.rotation; // make sure it faces right direction

            gameObject.SetActive(true);

            body.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            lifetimer += Time.deltaTime;
            if (lifetimer >= lifetime)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10);
            Destroy(gameObject);
            }

            
        }
    }
}
