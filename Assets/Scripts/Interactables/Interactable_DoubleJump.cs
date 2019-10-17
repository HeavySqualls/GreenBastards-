using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_DoubleJump : Interact_Base
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override void OnInteracted()
    {
        base.OnInteracted();
        pCon.DoubleJump();
        pCon.GetComponent<PlayerController>().interactableItem = null;
        pCon = null;
    }
}
