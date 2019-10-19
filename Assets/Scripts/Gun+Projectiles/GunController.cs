using UnityEngine;

public class GunController : MonoBehaviour
{
    [Space]
    [Header("Bullet Stats:")]
    public bool flipGunSprite;
    readonly float rotSpeed = 11.5f;
    readonly float rotLimit = 31f;

    [Space]
    [Header("Bullet Refrences:")]
    public Transform bulletPoint;
    public Transform bulletPointReversed;
    private GameObject bulletPrefab;

    public Transform muzzleFlashPoint;
    public Transform muzzleFlashPointReversed;
    private GameObject muzzleFlash;

    private SpriteRenderer spriteRenderer;
    private PlayerController pCon;

    void Start()
    {
        pCon = GetComponentInParent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletPrefab = Resources.Load<GameObject>("Bullet_Projectile_Prefab"); 
        muzzleFlash = Resources.Load<GameObject>("BulletFlashPrefab");
    }

    void Update()
    {
        //FaceMouse();
        FlipSprite();
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && pCon.ammo > 0)
        {
            if (!flipGunSprite)
            {
                Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
                Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
                pCon.ammo -= 1;
                pCon.UpdateAmmoUI();
            }
            else
            {
                Instantiate(bulletPrefab, bulletPointReversed.position, bulletPointReversed.rotation);
                Instantiate(muzzleFlash, muzzleFlashPointReversed.position, muzzleFlashPointReversed.rotation);
                pCon.ammo -= 1;
                pCon.UpdateAmmoUI();
            }
        }
    }

    void FaceMouse()
    {

        //Vector3 mouseCamPos = Camera.main.ScreenToViewportPoint(mousePos);
        //mouseCamPos = Vector3.Scale(mouseCamPos, new Vector3(Screen.Width/Screen.Height, 1, 1);
        //Vector2 direction = mouseCamPos - new Vector3(0.5, 0.5, 0);

        Vector3 stwp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(stwp);
        Vector2 direction = stwp - transform.position;

        float angle;
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
