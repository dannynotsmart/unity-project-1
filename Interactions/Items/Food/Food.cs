using Unity.VisualScripting;
using UnityEngine;

public class Food : Item
{
    public virtual float healthGained { get; set; }
    public virtual float staminaGained { get; set; }
    public virtual float badProbability { get; set; }
    
    private PlayerManager playerManager;
    private PlayerController playerController;
    private Inventory inventory;

    public AudioClip foodSound;
    private AudioSource audioSource;
    
    public void Start()
    {
        particleColor = Color.red;
        
        base.Start();
        
        transform.AddComponent<MeshCollider>();
        transform.GetComponent<MeshCollider>().convex = true;
        transform.AddComponent<Rigidbody>();
        
        playerManager = GameObject.Find("Manager").GetComponent<PlayerManager>();
        playerController = GameObject.Find("Controller").GetComponent<PlayerController>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    public override void Use()
    {
        audioSource.PlayOneShot(foodSound);
        float randomChance = Random.Range(0f, 1f);
        if (randomChance <= badProbability)
        {
            playerManager.currentHealth -= healthGained;
            playerController.AddStamina(-staminaGained);
        }
        else
        {
            playerManager.currentHealth += healthGained;
            playerManager.currentHealth = Mathf.Clamp(playerManager.currentHealth, 0, playerManager.maxHealth);
            playerController.AddStamina(staminaGained);
        }

        inventory.DestroyCurrent();
    }
}