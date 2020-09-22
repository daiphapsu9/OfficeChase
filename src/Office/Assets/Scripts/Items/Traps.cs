using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : CollectableItem
{

    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "boss")
        {
            var playerScript = other.GetComponent<ItemCollector>();

            //This is where you would pass the item to the Player script
            playerScript.OnPickupItem(this);

            Object.Destroy(gameObject);
        }
        base.OnTriggerEnter2D(other);
    }
}
