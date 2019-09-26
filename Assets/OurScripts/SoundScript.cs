using UnityEngine;

/* Script Author: Amie Faily
 * Edits by: Johan Appelgren
 */

public class SoundScript : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    AudioSource source;

    void OnEnable()
    {
        source.Stop();
        source.loop = true;
        source.spatialBlend = 1;
        source.clip = sounds[0];
        source.Play();
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Terrain"))
        {
            source.loop = false;
            source.spatialBlend = 0;
            source.clip = sounds[1];
            source.Play();
        }
    }
}