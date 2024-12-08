using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class VRMap_3
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Transform rigTargetOri;
    private Vector3 vrTargetOriPos;
    private Quaternion vrTargetOriRot;
    public Vector3 trackingRotationOffset;
    public float delay = 2.5f;
    [SerializeField] InputActionReference ActivateValueReference;
    public float activateValue;

    public float rigTargetScaleUp = 145f;


    public void SetUpVar()
    {
        vrTargetOriPos = vrTarget.position;
        vrTargetOriRot = vrTarget.rotation;
        rigTarget.position = rigTargetOri.position;
    }

    public void RightHandMap()
    {
        activateValue = ActivateValueReference.action.ReadValue<float>();
        rigTarget.position = rigTargetOri.position + rigTargetScaleUp * (vrTarget.position - vrTargetOriPos);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    public void LeftHandMap()
    {

    }
}



public class VRRig_3rdReview : MonoBehaviour
{

    public VRMap_3 rightHand;

    // Start is called before the first frame update
    void Start()
    {
        rightHand.SetUpVar();
    }

    // Update is called once per frame
    void Update()
    {
        rightHand.RightHandMap();
    }
}
