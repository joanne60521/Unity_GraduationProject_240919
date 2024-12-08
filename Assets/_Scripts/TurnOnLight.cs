using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    public StartGameControl startGameControl;

    public GameObject pointLight;
    public GameObject directLight;
    public GameObject enemyRobot;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;

    public AudioClip Light_on_loud;
    public AudioClip audio_Start;
    public AudioClip audio_Move;
    public AudioClip audio_Sight;
    public AudioClip audio_Gun;
    public AudioClip audio_Success;
    public AudioClip audio_Fail;

    private bool b = true;

    public bool moveBool = false;
    public bool sightBool = false;
    public bool gunBool = false;

    public MoveForwardByThumbstick moveForwardByThumbstick;
    public TurnHeadByThumbstick turnHeadByThumbstick;

    public int destroyCount = 0;
    public Arduino arduino;
    private bool endGame = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startGameControl.startGame && b)
        {
            b = false;
            pointLight.SetActive(true);
            directLight.SetActive(true);
            AudioSource.PlayClipAtPoint(Light_on_loud, new(0, -6, transform.position.normalized.z), 1f);

            Invoke("PlayStartAudio", 3f);
        }
        if (arduino.bulletCount <= arduino.maxBullet && destroyCount >= 5 && !endGame)
        {
            Invoke("PlaySuccessAudio", 2f);
            gunBool = false;
            endGame = true;
        }
        if (arduino.bulletCount >= arduino.maxBullet && destroyCount < 5&& !endGame)
        {
            Invoke("PlayFailAudio", 2f);
            gunBool = false;
            endGame = true;
        }
    }

    void PlayStartAudio()
    {
        AudioSource.PlayClipAtPoint(audio_Start, new(0, -6, transform.position.normalized.z), 1f);
        Invoke("PlayMoveAudio", 24f);
    }
    void PlayMoveAudio()
    {
        AudioSource.PlayClipAtPoint(audio_Move, new(0, -6, transform.position.normalized.z), 1f);
        moveBool = true;
        moveForwardByThumbstick.enabled = true;
        Invoke("PlaySightAudio", 17f);
    }
    void PlaySightAudio()
    {
        AudioSource.PlayClipAtPoint(audio_Sight, new(0, -6, transform.position.normalized.z), 1f);
        sightBool = true;
        turnHeadByThumbstick.enabled = true;
        Invoke("PlayGunAudio", 17f);

    }
    void PlayGunAudio()
    {
        enemyRobot.SetActive(true);
        target1.SetActive(true);
        target2.SetActive(true);
        target3.SetActive(true);
        target4.SetActive(true);
        target5.SetActive(true);
        AudioSource.PlayClipAtPoint(audio_Gun, new(0, -6, transform.position.normalized.z), 1f);
        Invoke("CanShoot", 32f);
    }
    void CanShoot()
    {
        gunBool = true;
    }


    void PlaySuccessAudio()
    {
        AudioSource.PlayClipAtPoint(audio_Success, new(0, -6, transform.position.normalized.z), 1f);
    }
    void PlayFailAudio()
    {
        AudioSource.PlayClipAtPoint(audio_Fail, new(0, -6, transform.position.normalized.z), 1f);
    }

}
