using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCrosshairRay : MonoBehaviour
{
    public LayerMask ui;
    public Transform muzzle;
    public RaycastHit crosshair;
    // Start is called before the first frame update
    void Start()
    {
        muzzle = GameObject.Find("Muzzle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(muzzle);
        if (Physics.Raycast(transform.position, transform.forward, out crosshair, ui))
        {
            Debug.DrawRay(transform.position, transform.forward * crosshair.distance, Color.yellow);
        }
    }
}
