using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GunController : MonoBehaviour
{
    [Space]
    [Header("Bullet Stats:")]
    public bool flipGunSprite;
    private float rotSpeed = 11.5f;
    private float rotLimit = 31f;

    [Space]
    [Header("Bullet Refrences:")]
    public Transform bulletPoint;
    public Transform bulletPointReversed;
    private GameObject bulletPrefab;

    public Transform muzzleFlashPoint;
    public Transform muzzleFlashPointReversed;
    private GameObject muzzleFlash;

    private SpriteRenderer spriteRenderer;
    //private PlayerController pCon;

    void Start()
    {
        //pCon = GetComponentInParent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletPrefab = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/Bullet_Prefab.prefab");
        muzzleFlash = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/BulletFlashPrefab.prefab");
    }

    void Update()
    {
        FaceMouse();
        FlipSprite();
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!flipGunSprite)
            {
                Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
                Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
            }
            else
            {
                Instantiate(bulletPrefab, bulletPointReversed.position, bulletPointReversed.rotation);
                Instantiate(muzzleFlash, muzzleFlashPointReversed.position, muzzleFlashPointReversed.rotation);
            }

        }
    }

    void FaceMouse()
    {
        float angle;

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (!flipGunSprite)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -rotLimit, rotLimit);
        }
        else
        {
            angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -rotLimit, rotLimit);
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
    }

    void FlipSprite()
    {
        if (flipGunSprite)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
