using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItemType
{
    Banana, // place trap
    Book // throw at boss

}
public class InventoryItem : CollectableItem
{
    public InventoryItemType type;
    override protected void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
