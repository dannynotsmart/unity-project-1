using TMPro;
using UnityEngine;

public class CameraInteraction : MonoBehaviour
{
    public GameObject player;
    public float reach = 2f;

    public TextMeshProUGUI label;
    
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, reach))
        {
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();
            if (interactableObject && interactableObject.interactable)
            {
                label.text = interactableObject.Display();
                float distance = Vector3.Distance(transform.position, hit.point);
                if (distance <= reach)
                {
                    interactableObject.OnRaycastContact(player);
                    if (interactableObject.IsKeyPressed())
                    {
                        interactableObject.Interact(player);
                    }
                }
            }
            else
            {
                label.text = "";
            }
        }
        else
        {
            label.text = "";
        }
    }
}