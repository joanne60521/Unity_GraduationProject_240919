using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public ParticleSystem explode;
    [SerializeField] private AudioClip audioo;
    public bool instan = false;



    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            explode.Play();
            AudioSource.PlayClipAtPoint(audioo, new(transform.position.x, -6, transform.position.z), 1f);
            instan = true;
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
