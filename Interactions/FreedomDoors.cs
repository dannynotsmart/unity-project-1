using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreedomDoors : Interactable
{
    public override KeyCode inputKey => KeyCode.Mouse0;
    
    public override void Interact(GameObject interactor)
    {
        if (interactor.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
