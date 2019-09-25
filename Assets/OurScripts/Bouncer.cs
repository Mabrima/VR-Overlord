using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBall"))
        {
            other.GetComponent<Rigidbody>().velocity = -other.GetComponent<Rigidbody>().velocity/4;
        }
    }
}
