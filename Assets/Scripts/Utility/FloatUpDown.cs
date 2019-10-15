using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    float originalY;

    public float floatStrength = 1f;                        

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Mathf.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}
