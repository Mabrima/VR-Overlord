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
    Rigidbody rb;

    private void OnEnable()
    {
        StartCoroutine(UpdateLightningPosition());
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Terrain") || collision.transform.CompareTag("Enemy"))
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            StartCoroutine(LightningStrike(collision));
        }
        else if (collision.transform.CompareTag("Floor"))
            ReturnLightning();
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
    IEnumerator LightningStrike(Collision col)
    {
        GetComponentInChildren<LightningBoltScript>().ChaosFactor = 0.3f;
        col.transform.GetComponent<UnitHealth>()?.TakeDamage(damage);

        yield return new WaitForSeconds(0.5f);

        GetComponentInChildren<LightningBoltScript>().ChaosFactor = 0.02f;
        rb.useGravity = true;
        gameObject.SetActive(false);
    }

    void ReturnLightning()
    {

        //code for returning lightning power to toolbox here

        rb.velocity = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
    }
}