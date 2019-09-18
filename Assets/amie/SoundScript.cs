using UnityEngine;

///Amie Faily

public class SoundScript : MonoBehaviour
{
    AudioSource ballAudio;
    string soundName;

    void Start()
    {
        ballAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        soundName = collision.gameObject.name;
        wichSound(soundName);
    }

    public void wichSound(string soundName)
    {
        switch (soundName)
        {
            case "Ball Touchdown adio":
                if (!ballAudio.isPlaying)
                    ballAudio.Play();
                break;

            default:
                if (!ballAudio.isPlaying)
                    ballAudio.Play();
                break;
        }
    }
}