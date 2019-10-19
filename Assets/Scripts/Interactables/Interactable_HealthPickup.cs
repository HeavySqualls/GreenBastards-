using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_HealthPickup : Interact_Base
{
    public int healthValue = 20;

    public override void Start()
    {
        base.Start();
        isInstantPickup = true;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.GetComponent<PlayerController>() && isInstantPickup)
        {
            pCon.health += healthValue;

            if (pCon.health > 100)
            {
                pCon.health = 100;
            }

            pCon.UpdateHealthUI();
            pCon.interactableItem = null;
            Destroy(gameObject);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    public override void OnInteracted()
    {
        base.OnInteracted();
        pCon.health += healthValue;
        if (pCon.health > 100)
        {
            pCon.health = 100;
        }
        pCon.UpdateHealthUI();
        pCon.interactableItem = null;
        Destroy(gameObject);
    }
}
