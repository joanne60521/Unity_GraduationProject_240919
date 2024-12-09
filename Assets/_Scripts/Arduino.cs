using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.InputSystem;
using TMPro;

public class Arduino : MonoBehaviour
{
    public SerialPort sp = new SerialPort("COM3", 9600);

    public InputActionReference rightActivateValueReference;
    private float rightActivateValue;
    private float nextTimeToFire = 0f;
    public float fireRate = 0.3f;
    private bool triggerPressed;
    public CameraShakeWhenFire cameraShakeWhenFire;
    public GunFire gunFire;
    public int bulletCount = 0;
    public int maxBullet = 50;
    public TextMeshProUGUI BulletNum;
    public TextMeshProUGUI MaxBulletNum;
    public TurnOnLight turnOnLight;

    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        MaxBulletNum.text = "/ " + maxBullet.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(sp.IsOpen && turnOnLight.gunBool) 
        {
            rightActivateValue = rightActivateValueReference.action.ReadValue<float>();

            if (rightActivateValue > 0.5 && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                if (triggerPressed)
                {
                    Debug.Log("fire");
                    sp.Write("1");  // shoot
                    gunFire.Shoot();
                    bulletCount++;
                    BulletNum.text = (maxBullet - bulletCount).ToString();
                    cameraShakeWhenFire.TriggerShake();
                    triggerPressed = false;
                }
            }
            if (rightActivateValue == 0)
            {
                triggerPressed = true;
            }
        }

        if (Input.GetKeyDown("a"))
        {
            gunFire.Shoot();
            bulletCount++;
            BulletNum.text = (maxBullet - bulletCount).ToString();
            cameraShakeWhenFire.TriggerShake();

        }
    }
}
