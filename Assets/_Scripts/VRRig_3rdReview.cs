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
    public InputActionReference velValueReference;
    private Vector3 velValue;
    public Vector3 vrTargetOriPos;
    private Quaternion vrTargetOriRot;
    public Vector3 trackingRotationOffset;
    public float delay = 2.5f;
    public float rigTargetScaleUp = 15f;


    public void SetUpVar()
    {
        vrTargetOriPos = vrTarget.position;
        vrTargetOriRot = vrTarget.rotation;
        rigTarget.position = rigTargetOri.position;
    }

    public void RightHandMap()
    {
        velValue = velValueReference.action.ReadValue<Vector3>();
        // if (velValue.magnitude > 0.1)
        // {
            rigTarget.position = Vector3.Lerp(rigTarget.position, rigTargetOri.position + rigTargetScaleUp * (vrTarget.position - vrTargetOriPos), delay * Time.deltaTime);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        // }else
        // {
        //     rigTarget.position = rigTarget.position;
        //     // rigTarget.rotation = rigTarget.rotation;
        // }
    }

    public void LeftHandMap()
    {

    }
}



public class VRRig_3rdReview : MonoBehaviour
{

    public VRMap_3 rightHand;
    public GameObject cubeeRed;
    public StartGameControl startGameControl;
    private bool setVarAlready = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startGameControl.startGame)
        {
            if (!setVarAlready)
            {
                setVarAlready = true;
                rightHand.SetUpVar();
            }else
            {
                rightHand.RightHandMap();
            }
        }
    }
}
