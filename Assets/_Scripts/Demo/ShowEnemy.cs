using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemy : MonoBehaviour
{
    public GameObject CubeEnemy;
    public Transform[] t;
    public Target target;
    public int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.instan)
        {
            Instantiate(CubeEnemy, t[Random.Range(0, 2)].position, Quaternion.identity);
            Debug.Log(t[Random.Range(0, 2)].position);
            target.instan = false;
        }
    }
}
