using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractInteract : MonoBehaviour
{
    public abstract void OnInteracted(Collider2D hit, Interact_Base interactable);
}
