using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.UI;

public class GunFire : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public InputActionReference rightSelectReference;
    private float rightSelectValue;
    [SerializeField] private AudioClip HandAttack;
    public Image GunCrosshair;
    public LayerMask layerEnemy;
    public ParticleSystem muzzleFlash;
    private float nextTimeToFire = 0f;
    public float fireRate = 5f;

    void Start()
    {

    }

    void Update()
    {
        // RaycastHit hitinfo;  //瞄到敵人時crosshair會放大未成功
        // Debug.DrawRay(transform.position, -transform.forward,Color.yellow);
        // if (Physics.Raycast(transform.position, -transform.forward, out hitinfo, layerEnemy))
        // {
        //     Debug.Log("hitinfo " + hitinfo.transform.name);
        //     GunCrosshair.transform.localScale = new(0.12f, 0.12f, 0.12f);
        // }

        rightSelectValue = rightSelectReference.action.ReadValue<float>();
        if (rightSelectValue == 1 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        AudioSource.PlayClipAtPoint(HandAttack, new(transform.position.x, -6, transform.position.z), 1f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, range))
        {
            Debug.Log("hit " + hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
