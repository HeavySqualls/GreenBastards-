﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character_Base
{
    [Space]
    [Header("Player Status:")]
    public bool pIsFlipped;
    public bool isInteractable;
    public bool isDead = false;

    [Space]
    [Header("Player Stats:")]
    public int ammo = 0;
    public float jumpTakeoffSpeed = 6f;
    public float maxSpeed = 2f;
    public float damageOutput;
    public int health = 10;

    [Space]
    [Header("Player Refrences:")]
    public Transform spawnZone;
    public GameObject interactableItem;
    private GunController gun;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gun = GetComponentInChildren<GunController>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        ComputeVelocity();
        FlipGun();
        Interaction();
    }

    public void Respawn()
    {       
        transform.position = spawnZone.position;
        isDead = false;
        animator.SetBool("isDead", isDead);
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        StartCoroutine(IFlashRed(spriteRenderer));

        if (health <= 0)
        {
            print("Player is DEAD");
            isDead = true;

            animator.SetBool("isDead", isDead);
        }
    }

    private void Interaction()
    {
        if (interactableItem != null && !isDead)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                print("Item picked up.");
                interactableItem.GetComponent<Interact_Base>().OnInteracted();
                print(ammo);
            }
        }
    }

    public void InteractableItem(GameObject _interactableObject)
    {
        interactableItem = _interactableObject;
    }

    protected override void ComputeVelocity()
    {
        if (!isDead)
        {
            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = jumpTakeoffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            bool flipPlayerSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
            if (flipPlayerSprite)
            {
                pIsFlipped = !pIsFlipped;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            animator.SetBool("grounded", isGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }        
    }

    private void FlipGun()
    {
        if (pIsFlipped)
        {
            gun.flipGunSprite = true;
        }
        else
        {
            gun.flipGunSprite = false;
        }
    }
}
