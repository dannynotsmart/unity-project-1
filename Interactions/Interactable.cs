using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public string name; // Interactable's name
    [SerializeField] public string description = ""; // Interactable's description
    [SerializeField] public virtual KeyCode inputKey { get; set; } // Interactable's input key to trigger interaction event
    [SerializeField] public bool interactable = true;
    
    public bool IsKeyPressed()
    {
        return Input.GetKeyDown(inputKey);
    }

    public virtual void OnRaycastContact(GameObject go) {}

    public virtual void Interact(GameObject interactor) {}

    public string Display()
    {
        string keyName = inputKey.ToString();
        if (keyName == "Mouse0") keyName = "Left Click";
        else if (keyName == "Mouse1") keyName = "Right Click";

        return $"{name} ({keyName})";
    }
}
