using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class CircleFillHandler : MonoBehaviour
{
    [Range(0, 30)]
    //public float fillValue = 0;//the falue of force
    public Image circleFillImage;
    public RectTransform handlerEdgeImage;
    public RectTransform fillHandler;

    //宣告另一個script
    CameraController cameraController; 

    // Start is called before the first frame update
    void Start()
    {
        //Find
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        FillCircleValue(cameraController.force);

        //Show variable
        Debug.Log("force pass: "+ cameraController.force);
    }

    void FillCircleValue(float value)
    {
        float fillAmount = (value / 30.0f);
        circleFillImage.fillAmount = fillAmount;
        float angle = fillAmount * 360;
        fillHandler.localEulerAngles = new Vector3(0, 0, -angle);
        handlerEdgeImage.localEulerAngles = new Vector3(0, 0, angle);
    }
}
