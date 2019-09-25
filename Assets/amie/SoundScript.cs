using UnityEngine;

/* Script Author: Amie Faily
 * Edits by: Johan Appelgren
 */

public class SoundScript : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    OVRGrabber hand;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hand != null && hand.grabbedFire)
        {
            hand.grabbedFire = false;
            PlayHeldSound();
        }
    }

    public void PlayHeldSound()
    {
        source.Stop();
        source.loop = true;
        source.clip = sounds[0];
        source.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<OVRGrabber>())
            hand = other.GetComponentInParent<OVRGrabber>();
        else if (other.CompareTag("Enemy") || other.CompareTag("Terrain"))
        {
            source.loop = false;
            source.clip = sounds[1];
            source.Play();
        }
    }
}