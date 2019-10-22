using System.Collections;
using UnityEngine;

public class Enemy_Crawler : Character_Base
{
    enum State { Patrolling, Dead }
    State currentState;

    [Space]
    [Header("Enemy Status:")]
    public bool isPatroller = true;
    private bool isMoving = false;
    private bool movingRight = true;
    private bool isDead = false;

    [Space]
    [Header("Enemy Stats:")]
    public float patrolSpeed = 0.5f;
    public int damageOutput = 1;
    public int health = 10;

    [Space]
    [Header("Enemy AI:")]
    public float pauseTime = 2f;
    private float detectionRange = 2f;
    private float wallDistance = 0.1f;
    private float groundDistance = 1f;
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
            case State.Patrolling:
                this.Patrolling();
                break;
            case State.Dead:
                this.Dead();
                break;
        }
    }


    // ---- STATES ---- //

    private void Patrolling()
    {
        Patrol();
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
        isMoving = false;
        animator.SetBool("moving", isMoving);

        yield return new WaitForSeconds(pauseTime);

        isMoving = true;
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

        DetectCollisions();
    }

    private void DetectCollisions()
    {
        // ---- CHECK FOR WALL ---- //

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

        if (wallInfo.collider != null)
        {
            if (!wallInfo.collider.isTrigger)
            {
                if (movingRight && !wallInfo.collider.GetComponent<PlayerController>())
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else if (!movingRight && !wallInfo.collider.GetComponent<PlayerController>())
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }

        // ---- CHECK FOR GROUND ---- //

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
        Debug.DrawRay(groundDetection.position, Vector2.down * groundDistance, Color.blue);

        // Checks for a loss of vertical collision (end of platform)
        if (!groundInfo.collider)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        if (movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            direction = Vector2.left;
            movingRight = false;
        }
        else if (!movingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = Vector2.right;
            movingRight = true;
        }
    }
}
