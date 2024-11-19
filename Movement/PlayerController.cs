using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerModel;
    public AnimationClip idleAnimation;
    public AnimationClip runningAnimation;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 2f;
    public float maxStamina = 100f;
    public float staminaDrainRate = 15f;
    public float staminaRegenRate = 10f;
    public float staminaRegenDelay = 2f;
    public float crouchHeight = 1f;
    public float normalHeight = 2f;
    public float crouchScaleY = 0.5f;
    public float gravity = -9.81f;

    private float stamina;
    private float speed;
    private float staminaRegenTimer = 0f;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private Vector3 velocity;
    private Animator animator;

    public float getStamina()
    {
        return stamina;
    }

    public void AddStamina(float amount)
    {
        stamina += amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    void Start()
    {
        stamina = maxStamina;
        speed = walkSpeed;
        controller.height = normalHeight;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchHeight;
            speed = crouchSpeed;
            playerModel.localScale = new Vector3(1, crouchScaleY, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = normalHeight;
            speed = walkSpeed;
            playerModel.localScale = Vector3.one;
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && !isCrouching)
        {
            isSprinting = true;
            speed = sprintSpeed;
            stamina -= staminaDrainRate * Time.deltaTime;
            staminaRegenTimer = 0f;
        }
        else
        {
            isSprinting = false;
            if (!isCrouching)
                speed = walkSpeed;
            
            staminaRegenTimer += Time.deltaTime;
        }

        if (!isSprinting && staminaRegenTimer >= staminaRegenDelay && stamina < maxStamina)
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (move.magnitude > 0)
        {
            animator.Play(runningAnimation.name);
        }
        else
        {
            animator.Play(idleAnimation.name);
        }
    }
}
