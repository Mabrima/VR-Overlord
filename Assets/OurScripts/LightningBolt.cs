using DigitalRuby.LightningBolt;
using System.Collections;
using UnityEngine;

/* Script Author: Johan Appelgren
 * Edits by:
 */

public class LightningBolt : MonoBehaviour
{
    [HideInInspector] public GameObject end;
    [HideInInspector] public GameObject prefab;
    [SerializeField] int damage = 10;
    OVRGrabber grabber;
    bool holdingLightning = false;


    private void OnEnable()
    {
        grabber = GetComponent<OVRGrabber>();
        StartCoroutine(UpdateLightningPosition());
    }

    void Update()
    {
        //every 10th frame, check if lightning has been grabbed.
        if (Time.frameCount % 10 == 0)
            holdingLightning = grabber.releasedLightning;
        //if it has...
        if (holdingLightning)
        {
            grabber.releasedLightning = false;
            StartCoroutine(LightningCoroutine());
        }
    }

    //Sets lightning end position to the ground
    IEnumerator UpdateLightningPosition()
    {
        do
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
                end.transform.position = hit.point;

            yield return new WaitForSeconds(0.05f);
        } while (true);
    }

    //Start this coroutine when the player releases the lightning bolt
    IEnumerator LightningCoroutine()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            prefab.GetComponent<LightningBoltScript>().ChaosFactor = 0.3f;
            hit.transform.GetComponent<UnitHealth>()?.TakeDamage(damage);

            yield return new WaitForSeconds(0.5f);

            prefab.GetComponent<LightningBoltScript>().ChaosFactor = 0.05f;
            gameObject.SetActive(false);
        }
        else
            ReturnLightning();
    }

    void ReturnLightning()
    {

        //code for returning lightning power to toolbox here

        gameObject.SetActive(false);
    }
}