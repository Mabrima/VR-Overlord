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

    void Start()
    {
        StartCoroutine(UpdateLightningPosition());
    }

    IEnumerator UpdateLightningPosition()
    {
        do
        {
            //Sets lightning end position to the ground
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
                end.transform.position = hit.point;
            yield return new WaitForSeconds(0.05f);
        } while (true);
    }

    public IEnumerator LightningStrike()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            prefab.GetComponent<LightningBoltScript>().ChaosFactor = 0.3f;
            hit.transform.GetComponent<UnitHealth>()?.TakeDamage(damage);

            yield return new WaitForSeconds(0.5f);

            prefab.GetComponent<LightningBoltScript>().ChaosFactor = 0.05f;
            gameObject.SetActive(false);
        }
    }
}
