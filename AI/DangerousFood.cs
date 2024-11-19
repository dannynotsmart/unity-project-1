using UnityEngine;

public class DangerousFood : MonoBehaviour
{
    private Transform playerTransform;
    public float detectionRadius = 0.5f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            PlayerManager playerManager = GameObject.Find("Manager").GetComponent<PlayerManager>();
            playerManager.currentHealth -= 5;
            Destroy(gameObject);
        }
    }
}