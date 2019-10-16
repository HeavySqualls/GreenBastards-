using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character_Base
{
    [Space]
    [Header("Player Status:")]
    public bool pIsFlipped;
    public bool isInteractable;
    public bool isDead = false;
    public bool isFrozen = true;

    [Space]
    [Header("Player Stats:")]
    public int ammo = 0;
    public int maxAmmo = 25;
    public float jumpTakeoffSpeed = 6f;
    public float maxSpeed = 2f;
    public float damageOutput;
    public int health = 100;

    [Space]
    [Header("Player Score:")]
    public int timesPlayerDied = 0;
    public int bulletsCollected = 0;
    public int enemiesKilled = 0;
    public float totalTime = 0;

    [Space]
    [Header("Player Refrences:")]
    public Transform spawnZone;
    public GameObject interactableItem;
    private GameManager gm;
    private GunController gun;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerUI pUI;

    void Awake()
    {
        gm = Toolbox.GetInstance().GetGameManager();
        pUI = GetComponentInChildren<PlayerUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gun = GetComponentInChildren<GunController>();
        animator = GetComponent<Animator>();

        pUI.SetAmmo(ammo);
    }

    protected override void Update()
    {
        base.Update();
        ComputeVelocity();
        FlipGun();
        Interaction();
    }

    public void DeliverScore()
    {
        gm.RecieveScore(timesPlayerDied, bulletsCollected, enemiesKilled);
    }

    public void Respawn()
    {
        print("Player is DEAD");
        transform.position = spawnZone.position;
        health = 100;
        pUI.SetHealth(health);
        isDead = false;
        timesPlayerDied++;
        animator.SetBool("isDead", isDead);
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        UpdateHealthUI();
        StartCoroutine(IFlashRed(spriteRenderer));

        if (health <= 0)
        {
            print("Player is DEAD");
            isDead = true;
            animator.SetBool("isDead", isDead);
        }
    }

    public void UpdateHealthUI()
    {
        pUI.SetHealth(health);     
    }

    public void UpdateAmmoUI()
    {
        pUI.SetAmmo(ammo);

        if (ammo <= 0)
        {
            pUI.None.SetActive(true);
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
        if (!isDead && !isFrozen)
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
