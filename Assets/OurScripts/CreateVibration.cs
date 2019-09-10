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

    public void CallVibration(float vibrationTime)
    {
        StartCoroutine(CreateVib(vibrationTime));
    }
    IEnumerator CreateVib(float vibrationTime)
    {
        OVRInput.SetControllerVibration(1, 1);
        yield return new WaitForSeconds(vibrationTime);
        Debug.Log("Vibration end");
        OVRInput.SetControllerVibration(0, 0);
    }
}
