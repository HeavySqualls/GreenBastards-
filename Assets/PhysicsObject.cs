using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float gravityModifier = 1f;

    protected Vector2 velocity;
    protected Rigidbody2D rb;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.up * deltaPosition.y;

        Movement(move);
    }

    void Movement(Vector2 move)
    {
        float distance = move.magnitude;
        rb.position = rb.position + move;
    }
}
