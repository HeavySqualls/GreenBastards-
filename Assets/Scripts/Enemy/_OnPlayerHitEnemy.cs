using UnityEngine;

public class _OnPlayerHitEnemy : EnemyInteractable
{
    private GameObject slashPrefab;
    private GameObject currentSlash;

    void Start()
    {
        slashPrefab = Resources.Load<GameObject>("Slash_Prefab"); 
    }

    public override void OnHit(Collision2D hit, EnemyController enemy)
    {
        if (hit.gameObject.GetComponent<PlayerController>().isDead == false)
        {
            currentSlash = Instantiate(slashPrefab, transform.position, transform.rotation);
            Destroy(currentSlash, 0.3f);
            enemy.StartChaseWait();
            hit.gameObject.GetComponent<PlayerController>().TakeDamage(enemy.damageOutput);
        }
        else if (hit.gameObject.GetComponent<PlayerController>().isDead == true)
        {
            enemy.GoPatrolling();
        }
    }
}
