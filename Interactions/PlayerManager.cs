using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private float maxStamina = 100f;
    public float currentStamina;

    public GameObject player;
    private PlayerController playerController;
    
    public Slider healthSlider;
    public Slider staminaSlider;
    
    void Start()
    {
        currentHealth = maxHealth;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        maxStamina = playerController.maxStamina;
        currentStamina = playerController.getStamina();

        UpdateSliders();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void UpdateSliders()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
    }
}