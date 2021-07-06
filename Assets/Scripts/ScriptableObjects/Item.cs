using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
public class Item : ScriptableObject
{
    public string objectName;
    public Sprite sprites;
    public int quantity;
    public bool stackable;
    public ItemType itemType;


    public enum ItemType
    {
        COIN, HEART
    }
    
}
