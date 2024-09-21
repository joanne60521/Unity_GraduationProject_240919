using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public VRRig_Robot VRRig_Robot;
    public bool reachedEnemy = false;
    [SerializeField] private AudioClip AttackHand;
    [SerializeField] private ParticleSystem explode;

    void Start()
    {
        VRRig_Robot = GameObject.Find("Arms test").GetComponent<VRRig_Robot>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (VRRig_Robot.leftHand.attacking | VRRig_Robot.rightHand.attacking)
        {
            reachedEnemy = true;
            AudioSource.PlayClipAtPoint(AttackHand, new(transform.position.x, -6, transform.position.z), 1f);
            explode.Play();
        }
    }
}
