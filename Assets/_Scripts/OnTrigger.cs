using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnTrigger : MonoBehaviour
{
    public string colTag = "rightCollider";
    public TextMeshProUGUI mytext;
    public bool attackMode = false;
    public VRRig_Big VRRig_Big;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == colTag)
        {
            Debug.Log(col.gameObject.tag + "attack mode");
            mytext.text = "attack mode";
            attackMode = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == colTag)
        {
            Debug.Log(col.gameObject.tag + "normal mode");
            mytext.text = "normal mode";
            if (!VRRig_Big.leftHand.attacking)
            {
                attackMode = false;
            }
        }
    }
}
