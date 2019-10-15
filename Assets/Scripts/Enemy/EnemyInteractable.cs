using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyInteractable : MonoBehaviour
{
    public abstract void OnHit(Collision2D hit, EnemyController enemy);
}
