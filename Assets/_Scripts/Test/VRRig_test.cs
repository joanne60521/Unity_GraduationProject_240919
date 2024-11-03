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
    public GameObject cubeEnemy;
    public EnemyCollider EnemyCollider;


    public float velocityValueThreshold = 0.5f;
    public float attackDelay = 2.5f;


    [HideInInspector]
    public float velocityValue = 0;
    [HideInInspector]
    public bool attacking = false;
    [HideInInspector]
    public bool attackMode = false;





    public void Map()
    {
        if (attackMode)
        {
            velocityValue = velocityReference.action.ReadValue<Vector3>().z;
            Debug.Log("controller velocity: " + velocityValue);
            if (velocityValue > velocityValueThreshold)
            {
                attacking = true;
            }

            if (attacking)
            {
                ////attack
                mytext.text = "> ATTACK";
                rigTarget.position = Vector3.Lerp(rigTarget.position, cubeEnemy.transform.position, attackDelay * Time.deltaTime);
                rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
            }else
            {
                ////normal move in attack ready area
                positionA = robotOrigin.transform.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOriginMainCam.position);
                rotatedPositionA = robotOrigin.transform.rotation * (positionA - robotOrigin.transform.position) + robotOrigin.transform.position;
                rigTarget.position = Vector3.Lerp(rigTarget.position, rotatedPositionA, delay * Time.deltaTime);
                rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
            }
            if (EnemyCollider.reachedEnemy)
            {
                Debug.Log("reached enemy");
                mytext.text = "> normal mode";
                attackMode = false;
                attacking = false;
                EnemyCollider.reachedEnemy = false;
            }
        }else
        {
            //normal mode
            positionA = robotOrigin.transform.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOriginMainCam.position);
            rotatedPositionA = robotOrigin.transform.rotation * (positionA - robotOrigin.transform.position) + robotOrigin.transform.position;
            rigTarget.position = Vector3.Lerp(rigTarget.position, rotatedPositionA, delay * Time.deltaTime);
            rigTarget.rotation = vrTarget.rotation * robotOrigin.transform.rotation * Quaternion.Euler(trackingRotationOffset);
            // Debug.Log(rigTarget.rotation + " = " + vrTarget.rotation + " * " + Quaternion.Euler(trackingRotationOffset) + " * " + robotOrigin.transform.rotation);
        }



        // // 以RobotOrigin為中心的手的位置
        // positionA = robotOrigin.transform.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOriginMainCam.position);
        // rotatedPositionA = robotOrigin.transform.rotation * (positionA - robotOrigin.transform.position) + robotOrigin.transform.position;
        // rigTarget.position = Vector3.Lerp(rigTarget.position, rotatedPositionA, delay * Time.deltaTime);
        // rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
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