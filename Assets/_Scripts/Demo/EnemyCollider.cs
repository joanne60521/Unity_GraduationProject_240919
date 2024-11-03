using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public VRRig_test VRRig_test;
    public bool reachedEnemy = false;
    public float blood = 10f;
    public HealthBar healthBar;
    [SerializeField] private AudioClip AttackHand;
    [SerializeField] private ParticleSystem explode;

    void Awake()
    {
        healthBar.maxHp = blood;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (VRRig_test.leftHand.attacking | VRRig_test.rightHand.attacking)
        {
            reachedEnemy = true;
            AudioSource.PlayClipAtPoint(AttackHand, new(transform.position.x, -6, transform.position.z), 1f);
            explode.Play();
            healthBar.hp --;
        }
    }
}
