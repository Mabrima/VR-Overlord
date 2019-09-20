using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltSpawner : MonoBehaviour
{
    [SerializeField] GameObject lightningBoltPrefab;
    [SerializeField] TextMesh number;
    public float cooldown;

    public void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

    void Update()
    {
        if (cooldown > 0)
            number.text = cooldown.ToString();
    }

    IEnumerator Cooldown()
    {
        cooldown = 5;
        do
        {
            cooldown -= Time.deltaTime;
            cooldown = cooldown <= 0 ? 0 : cooldown;
            yield return new WaitForSeconds(Time.deltaTime);
        } while (cooldown > 0);
        //do something to make it obvious that it can be used again
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller") && cooldown == 0)
        {
            lightningBoltPrefab.transform.position = transform.position;
            lightningBoltPrefab.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            lightningBoltPrefab.SetActive(true);
        }
    }
}
