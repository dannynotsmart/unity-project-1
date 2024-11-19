using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 3f;
    public float stopDistance = 1.5f;
    public float speed = 3f;

    void Update() 
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer < chaseRange && distanceToPlayer > stopDistance)
        {
            Chase();
        }
    }

    void Chase()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position - direction * stopDistance;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}