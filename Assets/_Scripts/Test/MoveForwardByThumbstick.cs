using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveForwardByThumbstick : MonoBehaviour
{
    [SerializeField] InputActionReference rightThumbstickReference;
    [SerializeField] float speed = 10;
    private float thumbstickX;
    private float thumbstickY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        thumbstickY = rightThumbstickReference.action.ReadValue<Vector2>().y;
        thumbstickX = rightThumbstickReference.action.ReadValue<Vector2>().x;

        if (thumbstickY > 0.5)
        {
            transform.position += transform.forward *Time.deltaTime * speed;
        }
        if (thumbstickY < -0.5)
        {
            transform.position += -transform.forward *Time.deltaTime * speed;
        }
        if (thumbstickX > 0.5)
        {
            transform.position += transform.right *Time.deltaTime * speed;
        }
        if (thumbstickX < -0.5)
        {
            transform.position += -transform.right *Time.deltaTime * speed;
        }
        
    }
}
