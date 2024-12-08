using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    [SerializeField] private AudioClip audioo;
    public bool instan = false;
    public GameObject targetObject;
    public GameObject particlePrefab;

    public TurnOnLight turnOnLight;



    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            GameObject particleInstance = Instantiate(particlePrefab, targetObject.transform.position, Quaternion.identity);
            Destroy(particleInstance, 0.83f); // 秒後銷毀

            AudioSource.PlayClipAtPoint(audioo, new(transform.position.normalized.x, -6, transform.position.normalized.z), 1f);
            turnOnLight.destroyCount++;
            instan = true;
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
