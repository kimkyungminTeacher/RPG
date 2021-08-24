using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HitPoints hitPoints;
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    private void Start()
    {
        hitPoints.value = startingHitPoints;

        healthBar = Instantiate(healthBarPrefab);
        inventory = Instantiate(inventoryPrefab);
        healthBar.character = this;
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

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints.value -= damage;
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        if (gameObject == null)
            return;

        base.KillCharacter();

        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        Start();
    }

    /*
    private void OnEnable()
    {
        ResetCharacter();
    }
    */
}
