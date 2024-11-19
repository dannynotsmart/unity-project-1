using UnityEngine;

public class CraftingTable : Interactable
{
    public GameObject keyPrefab;
    
    public override KeyCode inputKey => KeyCode.Mouse0;

    public AudioClip craftingSound;
    
    public override void Interact(GameObject interactor)
    {
        if (interactor.CompareTag("Player"))
        {
            GameManager gM = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (gM.ingotsCollected >= gM.maxRequired)
            {
                gM.ingotsCollected -= gM.maxRequired;
                Instantiate(keyPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
                
                var audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(craftingSound);
            }
        }
    }
}