using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] sounds;
    public AudioSource source;
    public static AudioManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        /*
        foreach (Sound s in sounds)
        {
            s.source = audioSource;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        */
    }

    public void Start()
    {
        GameMusic();
    }

    public void GameMusic()
    {
        source.Stop();
        source.clip = sounds[0];
        source.volume = 0.3f;
        source.loop = true;
        source.Play();
    }
    public void Victory()
    {
        source.clip = sounds[1];
        source.volume = 1;
        source.loop = false;
        source.Play();
    }

    /*
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }

        source.Play();
    }
    */
}
