using UnityEngine;

public class AIController : MonoBehaviour
{
    public float attackRange = 2f;
    public Transform player;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float fireCooldown = 1f;
    private float lastFireTime;

    public AudioClip audioClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < attackRange && Time.time > lastFireTime + fireCooldown)
        {
            AttackPlayer();
            lastFireTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            projectile.transform.rotation = Quaternion.LookRotation(direction);
            rb.velocity = direction * projectileSpeed;
        }
        audioSource.PlayOneShot(audioClip);
        Destroy(projectile, 2f);
    }
}