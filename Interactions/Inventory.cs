using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[3];
    public int currentItem = 0;
        
    public TextMeshProUGUI slot1;
    public TextMeshProUGUI slot2;
    public TextMeshProUGUI slot3;
    public TextMeshProUGUI desc;

    private Dictionary<int, TextMeshProUGUI> slots = new();

    public void Start()
    {
        slots.Add(0, slot1);
        slots.Add(1, slot2);
        slots.Add(2, slot3);
    }
    
    public void SetDescription()
    {
        desc.text = "";
    }
    
    public void SetDescription(GameObject go)
    {
        Interactable interactable = go.GetComponent<Interactable>();

        if (!interactable) return;

        desc.text = interactable.description;
    }
    
    public void SetSlot(int index, TextMeshProUGUI slot, bool bold = false)
    {
        string itemText = $"Slot {index + 1}:";
        
        if (bold)
        {
            itemText = $"<b>{itemText}</b>";
        }

        slot.text = itemText;
    }
    
    public void SetSlot(int index, TextMeshProUGUI slot, GameObject go, bool bold = false)
    {
        Interactable interactable = go.GetComponent<Interactable>();

        if (!interactable) return;

        string itemText = $"Slot {index + 1}: {interactable.name}";

        if (bold)
        {
            itemText = $"<b>{itemText}</b>";
        }

        slot.text = itemText;
    }

    public void Add(GameObject item)
    {
        Drop();
        
        inventory[currentItem] = item;
        
        var rb = item.GetComponent<Rigidbody>();
        // 
        // if (rb)
        // {
        //     rb.useGravity = false;
        //     rb.velocity = Vector3.zero;
        // }

        if (rb)
        {
            Destroy(rb);
        }
            
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        
        var interactable = item.GetComponent<Interactable>();
        interactable.interactable = false;
        
        var itemComponent = item.GetComponent<Item>();

        if (itemComponent)
        {
            itemComponent.usable = true;
        }
        
        SetSlot(currentItem, slots[currentItem], item, true);
        SetDescription(item);
    }

    public void Drop()
    {
        if (!inventory[currentItem])
            return;
        
        var current = inventory[currentItem];
        current.transform.SetParent(null);

        current.AddComponent<Rigidbody>();
        var currentRb = current.GetComponent<Rigidbody>();
        currentRb.useGravity = true;

        inventory[currentItem] = null;
        
        var interactable = current.GetComponent<Interactable>();
        interactable.interactable = true;
        
        var itemComponent = current.GetComponent<Item>();

        if (itemComponent)
        {
            itemComponent.usable = false;
        }
        
        SetSlot(currentItem, slots[currentItem], true);
        SetDescription();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
        
        int proposedItem = -1;
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) proposedItem = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) proposedItem = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) proposedItem = 2;

        if (currentItem == proposedItem || proposedItem == -1) return;

        if (inventory[currentItem])
        {
            inventory[currentItem].SetActive(false);
            
            var itemComponent = inventory[currentItem].GetComponent<Item>();

            if (itemComponent)
            {
                itemComponent.usable = false;
            }
            
            SetSlot(currentItem, slots[currentItem], inventory[currentItem]);
        }
        else
        {
            SetSlot(currentItem, slots[currentItem]);
        }

        if (inventory[proposedItem])
        {
            inventory[proposedItem].SetActive(true);
            
            var itemComponent = inventory[proposedItem].GetComponent<Item>();

            if (itemComponent)
            {
                itemComponent.usable = true;
            }
            
            SetSlot(proposedItem, slots[proposedItem], inventory[proposedItem], true);
            SetDescription(inventory[proposedItem]);
        }
        else
        {
            SetSlot(proposedItem, slots[proposedItem], true);
            SetDescription();
        }

        currentItem = proposedItem;
    }

    public void DestroyCurrent()
    {
        if (!inventory[currentItem]) return;
        
        Destroy(inventory[currentItem]);
        
        SetSlot(currentItem, slots[currentItem], true);
        SetDescription();
    }
}