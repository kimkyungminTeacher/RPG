using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public const int numSlots = 5;
    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots];
    GameObject[] slots = new GameObject[numSlots];
    private void createSlots()
    {
        if (slotPrefab == null)
        {
            return;
        }

        for (int i = 0; i < numSlots; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab);
            newSlot.name = "ItemSlot_" + i;

            newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);

            slots[i] = newSlot;
            itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
        }

    }

    void Start()
    {
        createSlots();
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                items[i].quantity = 1;

                itemImages[i].sprite = itemToAdd.sprites;
                itemImages[i].enabled = true;

                return true;
            } 
            else if (items[i].itemType == itemToAdd.itemType)
            {
                items[i].quantity += 1;

                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text qtyText = slotScript.qtyText;
                qtyText.enabled = true;
                qtyText.text = items[i].quantity.ToString();

                return true;
            }
        }
        return false;
    }
}
