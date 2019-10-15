using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class _OnPlayerHitEnemy : EnemyInteractable
{
    private GameObject slashPrefab;
    private GameObject currentSlash;

    void Start()
    {
        slashPrefab = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/Slash_Prefab.prefab");
    }

    public override void OnHit(Collision2D hit, EnemyController enemy)
    {
        if (hit.gameObject.GetComponent<PlayerController>())
        {
            currentSlash = Instantiate(slashPrefab, transform.position, transform.rotation);

            Destroy(currentSlash, 0.3f);
            enemy.StartChaseWait();
        }
    }
}
