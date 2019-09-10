using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateVibration : MonoBehaviour
{

    public float vibrationTime;

    public static CreateVibration singleton;

    private void Awake()
    {
        if (singleton)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    public void CallVibration(float vibrationTime, bool leftController, bool rightController)
    {
        StartCoroutine(CreateVib(vibrationTime, leftController, rightController));
    }
    IEnumerator CreateVib(float vibrationTime, bool leftController, bool rightController)
    {
        if (rightController) OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        if (leftController) OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(vibrationTime);
        if (rightController) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        if (leftController) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }
}
