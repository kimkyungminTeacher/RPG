using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    private void Start()
    {
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
        inventory = Instantiate(inventoryPrefab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item hitObject = collision.gameObject.GetComponent<Consumable>()?.item;
        if(hitObject != null)
        {
            print("Hit : " + hitObject.objectName);
            if (hitObject.itemType == Item.ItemType.HEART)
            {
                AdjustHitPoints(hitObject.quantity);
            } 
            else if (hitObject.itemType == Item.ItemType.COIN)
            {
                inventory.AddItem(hitObject);
            }
            collision.gameObject.SetActive(false);
        }
    }

    private void AdjustHitPoints(int quantity)
    {
        hitPoints.value += quantity;
        if (hitPoints.value > maxHitPoints)
        {
            quantity = (int)(hitPoints.value - maxHitPoints);
            hitPoints.value = maxHitPoints;
        }
        print("추가되는 체력 값 : " + quantity + ". 새로운 체력값 : " + hitPoints.value);
    }
}
