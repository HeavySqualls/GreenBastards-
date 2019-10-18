using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character_Base
{
    enum State { Patrolling, Hunting, Dead }
    State currentState;

    [Space]
    [Header("Enemy Status:")]
    public bool isPatroller = true;
    private bool isMoving = false;
    private bool movingRight = true;
    private bool enemyCharged = false;
    private bool chasePause = false;
    private bool isDead = false;

    [Space]
    [Header("Enemy Stats:")]
    public float patrolSpeed = 1.25f;
    public float huntSpeed = 3f;
    public int damageOutput = 2;
    public int health = 10;

    [Space]
    [Header("Enemy AI:")]
    public float pauseTime = 2f;
    private float detectionRange = 2f;
    private float wallDistance = 0.1f;
    private float groundDistance = 1f;
    private float distanceToTarget;
    private float minDistance = 0.3f;
    private float maxDistance = 4f;
    private Vector2 direction;

    [Space]
    [Header("Enemy References:")]
    public Transform eyeRange;
    public Transform groundDetection;
    private PlayerController pCon;
    private CapsuleCollider2D capsuleCol;
    private Animator animator;
    private GameObject target;

    void Start()
    {
        this.currentState = State.Patrolling;
        pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        capsuleCol = GetComponent<CapsuleCollider2D>();
    }

    protected override void Update()
    {
        base.Update();

        switch (this.currentState)
        {
            case State.Patrolling: this.Patrolling();
                break;
            case State.Hunting: this.Hunting();
                break;
            case State.Dead: this.Dead();
                break;
        }
    }


    // ---- STATES ---- //

    private void Patrolling()
    {
        Patrol();
    }

    private void Hunting()
    {
        // If enemy has not charged, get the current location of them, flip sprite if necessary and charge at their last known location.
        if (isMoving)
        {
            if (!enemyCharged)
            {
                chasePause = false;

                if (target.gameObject.transform.position.x > transform.position.x)
                    movingRight = false;
                else
                    movingRight = true;

                FlipSprite();
                enemyCharged = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(/*lastPos.x*/target.gameObject.transform.position.x, transform.position.y), huntSpeed * Time.deltaTime);

            DetectCollisions();
            CheckRayCast(direction);

            distanceToTarget = Vector2.Distance(transform.position, target.gameObject.transform.position);
            Debug.DrawRay(transform.position, direction * distanceToTarget, Color.yellow);

            if (distanceToTarget > maxDistance)
            {
                this.currentState = State.Patrolling;
            }
            else if (distanceToTarget <= minDistance && !chasePause)
            {
                StartChaseWait();
            }
        }
    }

    private void Dead()
    {
        // wait to be destroyed by take damage call
    }


    // ---- METHODS ---- //

    public void GoPatrolling()
    {
        this.currentState = State.Patrolling;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDead)
        {
            _OnPlayerHitEnemy pHitE = other.gameObject.GetComponent<_OnPlayerHitEnemy>();

            if (pHitE != null)
            {
                pHitE.OnHit(other, this);
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        StartCoroutine(IFlashRed(GetComponent<SpriteRenderer>()));

        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            gravityModifier = 0f;
            capsuleCol.enabled = false;
            pCon.enemiesKilled++;
            Destroy(gameObject, 3f);
            this.currentState = State.Dead;
        }
    }

    public void StartChaseWait()
    {
        StartCoroutine(ChaseWait());
    }

    private IEnumerator ChaseWait()
    {
        chasePause = true;
        isMoving = false;
        animator.SetBool("moving", isMoving);

        yield return new WaitForSeconds(pauseTime);

        enemyCharged = false;
        isMoving = true;
    }

    private void CheckRayCast(Vector2 direction)
    {
        int layer_mask = LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(eyeRange.position, direction * detectionRange, detectionRange, layer_mask);
        Debug.DrawRay(eyeRange.position, direction * detectionRange, Color.red);

        if (hit.collider != null && !hit.collider.isTrigger)
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                target = hit.collider.gameObject;
                this.currentState = State.Hunting;
            }
            else if(!hit.collider.GetComponent<PlayerController>() && target != null/* && !chasePause*/)
            {
                target = null;
                this.currentState = State.Patrolling;
            }
        }
    }

    private void Patrol()
    {
        if (isGrounded && isPatroller)
        {
            isMoving = true;
            transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);
            animator.SetBool("moving", isMoving);         
        }
        else
        {
            isMoving = false;
        }

        // Sends a cast out to search for the player
        CheckRayCast(direction);
        DetectCollisions();
    }

    private void DetectCollisions()
    {
        // Send cast to look for ground 
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
        Debug.DrawRay(groundDetection.position, Vector2.down * groundDistance, Color.blue);

        // Sets direction of casts
        RaycastHit2D wallInfo;

        if (!movingRight)
        {
            direction = Vector2.left; // If moving left, raycast left
            wallInfo = Physics2D.Raycast(groundDetection.position, direction, wallDistance);
            Debug.DrawRay(groundDetection.position, direction * wallDistance, Color.white);
        }
        else
        {
            direction = Vector2.right; // If moving right, raycast right
            wallInfo = Physics2D.Raycast(groundDetection.position, direction, wallDistance);
            Debug.DrawRay(groundDetection.position, direction * wallDistance, Color.white);
        }

        // Checks Vertical Collision
        if (!groundInfo.collider)
        {
            FlipSprite();
        }

        // Checks Horizontal Collision
        if (wallInfo.collider != null)
        {
            if (!wallInfo.collider.isTrigger)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
    }

    private void FlipSprite()
    {
        if (movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }
}
