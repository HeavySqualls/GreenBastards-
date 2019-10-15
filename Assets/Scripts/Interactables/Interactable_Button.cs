using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Button : Interact_Base
{
    public GameObject door;

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
        door.GetComponent<DoorController>().OpenDoor();

        pCon.interactableItem = null;
        interactKey.enabled = false;
        isInteractable = false;
    }

    private void OpenDoor()
    {
        door.GetComponent<DoorController>().OpenDoor();
    }
}
