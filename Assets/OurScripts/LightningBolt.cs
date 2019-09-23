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
    [SerializeField] OVRGrabber right;
    [SerializeField] OVRGrabber left;
    LightningBoltSpawner spawner;

    private void OnEnable()
    {
        spawner = FindObjectOfType<LightningBoltSpawner>();
        StartCoroutine(UpdateLightningPosition());
    }

    //Sets lightning end position to the ground
    IEnumerator UpdateLightningPosition()
    {
        do
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 4))
                end.transform.position = hit.point;

            if ((left.releasedLightning || right.releasedLightning) && hit.transform != null && (hit.transform.CompareTag("Terrain") || hit.transform.CompareTag("Enemy")))
            {
                left.releasedLightning = false;
                right.releasedLightning = false;
                spawner.StartCooldown();
                StartCoroutine(LightningStrike(hit));
            }
            else if (left.releasedLightning || right.releasedLightning)
            {
                left.releasedLightning = false;
                right.releasedLightning = false;
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