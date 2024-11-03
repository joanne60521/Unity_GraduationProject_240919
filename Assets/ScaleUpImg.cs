using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class ScaleUpImg : MonoBehaviour
{
    public VRRig_test vRRig_Test;
    public bool hideImg = false;
    private RectTransform rectTransform;
    private UnityEngine.UI.Image image;
    private Vector3 oriScale;
    [SerializeField]private  float delay = 2.5f;
    [SerializeField]private  float scaleSize = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<UnityEngine.UI.Image>();
        if (hideImg)
        {
            image.enabled = false;
        }
        oriScale = rectTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (vRRig_Test.leftHand.imgScaleUp | vRRig_Test.rightHand.imgScaleUp)
        {
            if (hideImg)
            {
                image.enabled = true;
            }
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, oriScale * scaleSize, Time.deltaTime * delay);
        }else
        {
            if (hideImg)
            {
                image.enabled = false;
            }
            rectTransform.localScale = oriScale;
        }
    }
}
