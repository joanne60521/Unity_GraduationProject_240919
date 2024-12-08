using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveForwardByThumbstick : MonoBehaviour
{
    [SerializeField] InputActionReference leftThumbstickReference;
    [SerializeField] float speed = 10;
    private float thumbstickX;
    private float thumbstickY;
    private CharacterController characterController;
    public Transform vrCamera;

    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // thumbstickY = leftThumbstickReference.action.ReadValue<Vector2>().y;
        // thumbstickX = leftThumbstickReference.action.ReadValue<Vector2>().x;

        // if (thumbstickY > 0.5)
        // {
        //     transform.position += transform.forward *Time.deltaTime * speed;
        // }
        // if (thumbstickY < -0.5)
        // {
        //     transform.position += -transform.forward *Time.deltaTime * speed;
        // }
        // if (thumbstickX > 0.5)
        // {
        //     transform.position += transform.right *Time.deltaTime * speed;
        // }
        // if (thumbstickX < -0.5)
        // {
        //     transform.position += -transform.right *Time.deltaTime * speed;
        // }


        Vector2 input = leftThumbstickReference.action.ReadValue<Vector2>();

        Vector3 forward = vrCamera.forward;
        forward.y = 0;
        Vector3 movement = (forward * input.y + vrCamera.right * input.x).normalized;

        characterController.Move(movement * speed * Time.deltaTime);
        
    }
}
