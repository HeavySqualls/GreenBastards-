using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerHitInteractable : AbstractInteract
{
    public override void OnInteracted(Collider2D hit, Interact_Base self)
    {
        if (hit.gameObject.GetComponent<PlayerController>())
        {
            self.interactKey.enabled = true;
            hit.GetComponent<PlayerController>().InteractableItem(self.gameObject);
        }
    }
}
