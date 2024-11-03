using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


[System.Serializable]  //讓 class VRMap 顯示在 Inspector
public class VRMap_test
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public GameObject robotOrigin;
    public Transform playerOriginMainCam;
    public float scaleUp = 12.5f;
    public float delay = 2.5f;

    [HideInInspector]
    public Vector3 positionA;
    [HideInInspector]
    public Vector3 rotatedPositionA;



    // attack mode
    public InputActionReference velocityReference;
    public TextMeshProUGUI mytext;

    [HideInInspector]
    public float velocityValue = 0;
    [HideInInspector]
    public bool attacking = false;
    [HideInInspector]
    public bool attackMode = false;





    public void Map()
    {
        velocityValue = velocityReference.action.ReadValue<Vector3>().z;




        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        // 以RobotOrigin為中心的手的位置
        positionA = robotOrigin.transform.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOriginMainCam.position);
        rotatedPositionA = robotOrigin.transform.rotation * (positionA - robotOrigin.transform.position) + robotOrigin.transform.position;
        rigTarget.position = Vector3.Lerp(rigTarget.position, rotatedPositionA, delay * Time.deltaTime);
    }


    public void MapHead()
    {
        rigTarget.position = robotOrigin.transform.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOriginMainCam.position);
        // rigTarget.rotation = headTurnCam.rotation * Quaternion.Euler(trackingRotationOffset);
        // rigTarget.position = Vector3.Lerp(rigTarget.position, robotOrigin.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOriginMainCam.position), delay * Time.deltaTime);


    }
}

public class VRRig_test : MonoBehaviour
{
    public float turnSmoothness = 5;

    public VRMap_test head;
    public VRMap_test leftHand;
    public VRMap_test rightHand;
    public GameObject robotOrigin;
    public GameObject cubeeRed;
    public GameObject cubeeBlue;


    public Transform headConstraint;
    private Vector3 headBodyOffset;
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    void LateUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        // transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.MapHead();
        leftHand.Map();
        rightHand.Map();

        transform.rotation = robotOrigin.transform.rotation;

        if (Input.GetKeyDown("space"))
        {
            Instantiate(cubeeRed, leftHand.positionA, transform.rotation);
            Instantiate(cubeeBlue, leftHand.rotatedPositionA, transform.rotation);
        }
    }
}