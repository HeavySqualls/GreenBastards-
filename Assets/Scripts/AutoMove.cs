using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : PhysicsObject
{
    void Update()
    {
        targetVelocity = Vector2.left;
    }
}
