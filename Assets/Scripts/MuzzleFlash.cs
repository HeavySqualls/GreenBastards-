using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 0.04f);
    }
}
