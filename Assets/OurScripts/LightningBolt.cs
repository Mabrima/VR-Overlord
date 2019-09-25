using DigitalRuby.LightningBolt;
using System.Collections;
using UnityEngine;

/* Script Author: Johan Appelgren
 * Edits by:
 */

public class LightningBolt : MonoBehaviour
{
    [HideInInspector] public GameObject end;
    [SerializeField] AudioClip[] sounds;
    AudioSource source;
    [SerializeField] int damage = 10;
    [SerializeField] OVRGrabber right;
    [SerializeField] OVRGrabber left;
    LightningBoltSpawner spawner;

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponentInChildren<LineRenderer>().enabled = true;
        StartCoroutine(UpdateLightningPosition());
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
        spawner = FindObjectOfType<LightningBoltSpawner>();
    }

    public void PlayHeldSound()
    {
        source.Stop();
        source.loop = true;
        source.clip = sounds[0];
        source.Play();
    }

    //Sets lightning end position to the ground
    IEnumerator UpdateLightningPosition()
    {
        do
        {
            if (right.grabbedLightning)
            {
                right.grabbedLightning = false;
                PlayHeldSound();
            }
            if (left.grabbedLightning)
            {
                left.grabbedLightning = false;
                PlayHeldSound();
            }

            if (Physics.SphereCast(transform.position, 0.1f, transform.forward, out RaycastHit hit, 4))
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
        source.loop = false;
        source.clip = sounds[1];
        source.Play();

        GetComponentInChildren<LightningBoltScript>().ChaosFactor = 0.3f;
        obj.transform.GetComponent<UnitHealth>()?.TakeDamage(damage);

        yield return new WaitForSeconds(0.75f);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<LineRenderer>().enabled = false;
        GetComponentInChildren<LightningBoltScript>().ChaosFactor = 0.02f;

        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    void ReturnLightning()
    {
        //code for returning lightning power to toolbox here

        gameObject.SetActive(false);
    }
}