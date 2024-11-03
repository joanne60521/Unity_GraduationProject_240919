using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnTrigger_Demo : MonoBehaviour
{
    public string colTag = "////////";
    public TextMeshProUGUI mytext;
    public VRRig_Robot VRRig_Robot;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(colTag))
        {
            Debug.Log(col.gameObject.tag + " : attack mode");
            mytext.text = "> attack mode";
            if (colTag == "leftCollider")
            {
                VRRig_Robot.leftHand.attackMode = true;
            }
            if (colTag == "rightCollider")
            {
                VRRig_Robot.rightHand.attackMode = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == colTag)
        {
            if (!VRRig_Robot.leftHand.attacking)
            {
                mytext.text = "> normal mode";
                VRRig_Robot.leftHand.attackMode = false;
            }
            if (!VRRig_Robot.rightHand.attacking)
            {
                mytext.text = "> normal mode";
                VRRig_Robot.rightHand.attackMode = false;
            }
        }
    }
}
