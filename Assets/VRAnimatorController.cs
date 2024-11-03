using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedThreshold = 0.1f;
    public GameObject RobotOrigin;
    private Animator animator;
    private Vector3 previousPos;
    private VRRig_test vRRig_Test;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vRRig_Test = GetComponent<VRRig_test>();
        // previousPos = vRRig_Test.head.rigTarget.position;
        previousPos = RobotOrigin.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // // compute the speed
        // Vector3 headsetSpeed = (vRRig_Test.head.rigTarget.position - previousPos) / Time.deltaTime;
        // headsetSpeed.y = 0;

        // // local speed
        // Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        // previousPos = vRRig_Test.head.rigTarget.position;

        // // set animator values
        // animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedThreshold);
        // print(vRRig_Test.head.rigTarget.position);

        // use RobotOrigin as origin
        Vector3 RobotOriginSpeed = RobotOrigin.transform.position - previousPos;
        RobotOriginSpeed.y = 0;
        previousPos = RobotOrigin.transform.position;
        animator.SetBool("isMoving", RobotOriginSpeed.magnitude > speedThreshold);
    }
}
