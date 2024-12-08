using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeWhenFire : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

    private Vector3 originalPosition;
    private float remainingShakeTime;

    void OnEnable()
    {
        // Save the initial camera position
        originalPosition = transform.localPosition;
    }

    public void TriggerShake(float duration = 0, float magnitude = 0)
    {
        remainingShakeTime = duration > 0 ? duration : shakeDuration;
        shakeMagnitude = magnitude > 0 ? magnitude : shakeMagnitude;
    }

    void Update()
    {
        if (remainingShakeTime > 0)
        {
            // Randomly shake the camera around its original position
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            remainingShakeTime -= Time.deltaTime;
        }
        else
        {
            // Reset to the original position
            transform.localPosition = originalPosition;
        }
    }
}
