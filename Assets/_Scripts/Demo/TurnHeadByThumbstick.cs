using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnHeadByThumbstick : MonoBehaviour
{
    [SerializeField] InputActionReference leftControllerReference;
    [SerializeField] float turnValue = 20;
    private float thumbstickX;
    private float thumbstickY;
    public GameObject RobotOrigin;


    void Start()
    {

    }

    void Update()
    {
        //Debug.Log(leftControllerReference.action.ReadValue<Vector2>());
        thumbstickX = leftControllerReference.action.ReadValue<Vector2>().x;
        thumbstickY = leftControllerReference.action.ReadValue<Vector2>().y;


        if (Mathf.Abs(thumbstickX) > 0.2)
        {
            transform.eulerAngles += new Vector3(0, turnValue * Time.deltaTime * thumbstickX, 0);
        }
        if (Mathf.Abs(thumbstickY) > 0.2)
        {
            // Debug.Log(mytransform.rotation.x);
            transform.eulerAngles += new Vector3(-turnValue * Time.deltaTime * thumbstickY, 0, 0);
        }


        transform.position = RobotOrigin.transform.position;
    }
}
