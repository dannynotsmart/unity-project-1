using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public override KeyCode inputKey => KeyCode.Mouse0;
    public GameObject otherDoor;

    public AudioClip doorSound;
    
    public override void Interact(GameObject interactor)
    {
        if (interactor.CompareTag("Player"))
        {
            Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

            if (inventory.inventory[inventory.currentItem])
            {
                if (inventory.inventory[inventory.currentItem].CompareTag("Key"))
                {
                    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                    audioSource.PlayOneShot(doorSound);
                    
                    inventory.DestroyCurrent();
                    Destroy(transform.parent.gameObject);
                    Destroy(otherDoor);
                }
            }
        }
    }
}
