using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_AmmoPickup : Interact_Base
{
    public int ammoValue = 5;

    public override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    public override void OnInteracted()
    {
        base.OnInteracted();
        pCon.ammo += ammoValue;
        if (pCon.ammo > pCon.maxAmmo)
        {
            pCon.ammo = pCon.maxAmmo;
        }
        pCon.UpdateAmmoUI();
        pCon.bulletsCollected += ammoValue;
        pCon.interactableItem = null;
        Destroy(gameObject);
    }
}
