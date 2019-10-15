using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character_Base
{
    enum State { Patrolling, Hunting, Attacking, Death}
    State currentState;

    [Space]
    [Header("Enemy Status:")]
    public bool isPatroller = true;
    private bool isMoving = false;
    private bool movingRight = true;
    private bool enemyCharged = false;
    private bool chasePause = false;

    [Space]
    [Header("Enemy Stats:")]
    public float patrolSpeed = 1.25f;
    public float huntSpeed = 3f;
    public float damageOutput;
    public float health;
    public float pauseTime = 2f;
    private float detectionRange = 2f;
    float distanceToTarget;
    float minDistance = 1f;
    float maxDistance = 4f;

    Vector2 targetDir;
    Vector2 lastPos;

    [Space]
    [Header("Enemy References:")]
    public Transform eyeRange;
    public Transform groundDetection;
    private Animator animator;
    private GameObject target;

    void Start()
    {
        this.currentState = State.Patrolling;
        animator = GetComponent<Animator>();
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
            case State.Attacking: this.Attacking();
                break;
            case State.Death: this.Death();
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
        if (!enemyCharged)
        {
            chasePause = false;
            lastPos = new Vector2(target.transform.position.x, target.transform.position.y);

            enemyCharged = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(lastPos.x, transform.position.y), huntSpeed * Time.deltaTime); // TODO: Get player to rotate and face them when re-targeting

        distanceToTarget = Vector2.Distance(transform.position, lastPos);
        if (distanceToTarget > maxDistance)
        {
            this.currentState = State.Patrolling;
        }
        else if (distanceToTarget <= minDistance && !chasePause)
        {
            StartChaseWait();
        }
    }

    private void Attacking()
    {

    }

    private void Death()
    {

    }


    // ---- METHODS ---- //

    private void OnCollisionEnter2D(Collision2D other)
    {
        _OnPlayerHitEnemy pHitE = other.gameObject.GetComponent<_OnPlayerHitEnemy>();

        if (pHitE != null)
        {
            pHitE.OnHit(other, this);
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
                print("Hunting");
                target = hit.collider.gameObject;
                this.currentState = State.Hunting;
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

            // Send cast to look for ground 
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
            Debug.DrawRay(groundDetection.position, Vector2.down * 2f, Color.blue);

            // Sets direction of casts
            Vector2 direction;
            RaycastHit2D wallInfo;

            if (!movingRight)
            {
                direction = Vector2.left; // If moving left, raycast left
                wallInfo = Physics2D.Raycast(groundDetection.position, direction, 0.25f);
                Debug.DrawRay(groundDetection.position, direction * 0.25f, Color.white);
            }
            else
            {               
                direction = Vector2.right; // If moving right, raycast right
                wallInfo = Physics2D.Raycast(groundDetection.position, direction, 0.25f);
                Debug.DrawRay(groundDetection.position, direction * 0.25f, Color.white);
            }

            // Sends a cast out to search for the player
            CheckRayCast(direction);

            // Checks Vertical Collision
            if (!groundInfo.collider)
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
        else
        {
            isMoving = false;
        }
    }
}
