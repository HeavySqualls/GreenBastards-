using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // HEALTH BAR
    public Image healthBar;
    public int MinHealth;
    public int MaxHealth;
    private int mCurrentValue;
    private float mCurrentPercent;

    // AMMO BAR 
    public Image ammoBar;
    public GameObject None;
    public int MinAmmo;
    public int MaxAmmo;
    private GameObject startText;
    private int mCurrentAmmoValue;
    private float mCurrentAmmoPercent;

    private bool gameStart = false;
    private PlayerController pCon;

    void Start()
    {
        startText = GameObject.FindGameObjectWithTag("StartText");
        pCon = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && gameStart == false)
        {
            startText.SetActive(false);
            Toolbox.GetInstance().GetTimeManager().StartTimeTracker();
            pCon.isFrozen = false;
            gameStart = true;
        }
    }

    public void SetHealth(int _health)
    {
        if (_health != mCurrentValue)
        {
            if (MaxHealth - MinHealth == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = _health;
                mCurrentPercent = (float)mCurrentValue / (float)(MaxHealth - MinHealth);
            }
          
            healthBar.fillAmount = mCurrentPercent;
        }
    }

    public void SetAmmo(int _Ammo)
    {
        if (_Ammo != mCurrentAmmoValue)
        {
            if (MaxAmmo - MinAmmo == 0)
            {
                mCurrentAmmoValue = 0;
                mCurrentAmmoPercent = 0;
            }
            else
            {
                None.SetActive(false);
                mCurrentAmmoValue = _Ammo;
                mCurrentAmmoPercent = (float)mCurrentAmmoValue / (float)(MaxAmmo - MinAmmo);
            }

            ammoBar.fillAmount = mCurrentAmmoPercent;
        }
    }
}
