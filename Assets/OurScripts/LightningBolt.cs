using DigitalRuby.LightningBolt;
using System.Collections;
using UnityEngine;

/* Script Author: Johan Appelgren
 * Edits by:
 */

public class LightningBolt : MonoBehaviour
{
    [HideInInspector] public GameObject end;
    [SerializeField] int damage = 10;
    OVRGrabber hand;
    LightningBoltSpawner spawner;

    private void OnEnable()
    {
        spawner = FindObjectOfType<LightningBoltSpawner>();
        hand = FindObjectOfType<OVRGrabber>();
        StartCoroutine(UpdateLightningPosition());
    }

    //Sets lightning end position to the ground
    IEnumerator UpdateLightningPosition()
    {
        do
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 4))
                end.transform.position = hit.point;

            if (hand.releasedLightning && hit.transform != null && (hit.transform.CompareTag("Terrain") || hit.transform.CompareTag("Enemy")))
            {
                hand.releasedLightning = false;
                spawner.StartCooldown();
                StartCoroutine(LightningStrike(hit));
            }
            else if (hand.releasedLightning)
            {
                hand.releasedLightning = false;
                ReturnLightning();
            }

            yield return new WaitForSeconds(0.05f);
        } while (true);
    }

    //Start this coroutine when the player releases the lightning bolt
    IEnumerator LightningStrike(RaycastHit obj)
    {
        GetComponentInChildren<LightningBoltScript>().ChaosFactor = 0.3f;
        obj.transform.GetComponent<UnitHealth>()?.TakeDamage(damage);

        yield return new WaitForSeconds(0.75f);

        GetComponentInChildren<LightningBoltScript>().ChaosFactor = 0.02f;
        gameObject.SetActive(false);
    }

    void ReturnLightning()
    {
        //code for returning lightning power to toolbox here

        gameObject.SetActive(false);
    }
}