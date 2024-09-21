using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public class VRMap_
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public Transform smallOri;
    public Transform bigOri;
    public float delay = 2.5f;
    public OnTrigger OnTrigger;
    public Transform cubeEnemy;
    [SerializeField] InputActionReference leftVelReference;
    [SerializeField] InputActionReference leftSelectlReference;
    public float leftVel;
    public float leftSelect;
    public bool attacking = false;

    public void Map()
    {
        leftVel = leftVelReference.action.ReadValue<Vector3>().z;
        leftSelect = leftSelectlReference.action.ReadValue<float>();

        if (OnTrigger.attackMode && leftSelect != 0)
        {
            attacking = true;
            Debug.Log("b");
            rigTarget.position = Vector3.Lerp(rigTarget.position, cubeEnemy.position, delay * Time.deltaTime);
            if (rigTarget.position == cubeEnemy.position)
            {
                OnTrigger.attackMode = false;
                attacking = false;
                Debug.Log("a");
            }
        }
        else
        {
            rigTarget.position = Vector3.Lerp(rigTarget.position, bigOri.position + 145 * (vrTarget.TransformPoint(trackingPositionOffset) - smallOri.position), delay * Time.deltaTime);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        }
    }
    public void MapHead()
    {
        // rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig_Big : MonoBehaviour
{
    public VRMap_ head;
    public VRMap_ leftHand;
    public VRMap_ rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;
    public GameObject cubeeRed;
    public GameObject cubeeBlue;
    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
        //headBodyOffset = (-0.17, -224.94, -0.96)
        //transform.position = (0.00, -160.00, 30.00)        = 腳底位置
        //headConstraint.position = (0.17, 64.94, 30.96)     = mainCam
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = headConstraint.position + headBodyOffset;
        //Debug.Log(transform.position);         //(0.00, -160.00, 30.00)   -->   (-0.17, -189.94, -0.96) = transform - headConstraint
        //Debug.Log(headConstraint.position);    //(0.17, 64.94, 30.96)
        //Debug.Log(headBodyOffset);             //(-0.17, -224.94, -0.96)

        //transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;

        head.MapHead();
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

