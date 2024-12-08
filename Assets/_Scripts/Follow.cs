using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform followThis;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - followThis.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followThis.position + offset;
    }
}
