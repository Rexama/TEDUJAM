using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMe : MonoBehaviour
{
    private float rotationSpeed = 30f;

    void Update()
    {
        float angle = Mathf.Sin(Time.time) * rotationSpeed;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
