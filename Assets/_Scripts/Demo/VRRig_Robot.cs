using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[System.Serializable]
public class VRMap_RobotArm
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public Transform playerOrigin;
    public Transform robotOrigin;

    public float scaleUp = 12.5f;
    public float delay = 2.5f;
    public float attackDelay = 2.5f;
    public OnTrigger_Demo OnTrigger_Demo;
    public EnemyCollider EnemyCollider;
    public Transform cubeEnemy;
    public InputActionReference velocityReference;
    public InputActionReference selectReference;
    public bool attacking = false;
    public bool attackMode = false;
    public TextMeshProUGUI mytext;

    private float velocityValue = 0;
    private float selectValue;

    public void Map()
    {
        if (attackMode)
        {
            velocityValue = velocityReference.action.ReadValue<Vector3>().z;
            Debug.Log("controller velocity: " + velocityValue);
            if (velocityValue > 0.5)
            {
                attacking = true;
            }

            if (attacking)
            {
                ////attack
                mytext.text = "> ATTACK";
                rigTarget.position = Vector3.Lerp(rigTarget.position, cubeEnemy.position, attackDelay * Time.deltaTime);
                rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
            }else
            {
                ////normal move in attack ready area
                rigTarget.position = Vector3.Lerp(rigTarget.position, robotOrigin.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOrigin.position), delay * Time.deltaTime);
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
        }
        else
        {
            //normal mode
            rigTarget.position = Vector3.Lerp(rigTarget.position, robotOrigin.position + scaleUp * (vrTarget.TransformPoint(trackingPositionOffset) - playerOrigin.position), delay * Time.deltaTime);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        }
    }
}




public class VRRig_Robot : MonoBehaviour
{
    public VRMap_RobotArm leftHand;
    public VRMap_RobotArm rightHand;

    // public Transform headConstraint;
    public Vector3 headBodyOffset;
    public GameObject cubeeRed;
    public GameObject cubeeBlue;
    // Start is called before the first frame update
    void Start()
    {
        // headBodyOffset = transform.position - headConstraint.position;
        //headBodyOffset = (-0.17, -224.94, -0.96)
        //transform.position = (0.00, -160.00, 30.00)        = 腳底位置
        //headConstraint.position = (0.17, 64.94, 30.96)     = mainCam
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = headConstraint.position + headBodyOffset;
        //Debug.Log(transform.position);
        //Debug.Log(headConstraint.position);
        //Debug.Log(headBodyOffset);

        //transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;

        leftHand.Map();
        rightHand.Map();

        if (Input.GetKeyDown("space"))
        {
            Instantiate(cubeeRed, leftHand.vrTarget.position, leftHand.vrTarget.rotation);
            Instantiate(cubeeBlue, leftHand.rigTarget.position, leftHand.rigTarget.rotation);
            Instantiate(cubeeRed, rightHand.vrTarget.position, rightHand.vrTarget.rotation);
            Instantiate(cubeeBlue, rightHand.rigTarget.position, rightHand.rigTarget.rotation);
        }
    }
}
