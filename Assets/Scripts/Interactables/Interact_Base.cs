using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact_Base : MonoBehaviour
{
    protected Canvas canvas;
    protected bool isInteractable = true;
    protected bool isInstantPickup;

    public Image interactKey;

    protected PlayerController pCon;

    public virtual void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        interactKey = canvas.GetComponentInChildren<Image>();

        interactKey.enabled = false;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        _PlayerHitInteractable pHitI = other.GetComponent<_PlayerHitInteractable>();

        if (pHitI != null && isInteractable)
        {
            pHitI.OnInteracted(other, this);
            pCon = other.GetComponent<PlayerController>();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        _PlayerHitInteractable pHitI = other.GetComponent<_PlayerHitInteractable>();

        if (pHitI != null)
        {
            interactKey.enabled = false;
            other.GetComponent<PlayerController>().interactableItem = null;
            pCon = null;
            pHitI = null;
        }
    }

    public virtual void OnInteracted() { }
}
