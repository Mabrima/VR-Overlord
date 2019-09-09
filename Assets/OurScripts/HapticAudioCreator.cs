using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Author: Robin

public class HapticAudioCreator : MonoBehaviour
{
    public static HapticAudioCreator singleton;

    private void Awake()
    {
        if (singleton)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    /// <summary>
    /// Takes in 3 ints
    /// Iteration; how long the clip will be, 360 is a full second as the controllers run at 360HZ.
    /// Frequency; how often of these 360HZ it will rumble, ex 2 will run it every other, 4 every 4th and so on.
    /// Strength; how strong the vibration will be when it triggers 0-255.
    /// </summary>
    /// <param name="iteration"></param>
    /// <param name="frequency"></param>
    /// <param name="strength"></param>
    /// <returns></returns>
    public OVRHapticsClip CreateHapticAudio(int iteration, int frequency, int strength)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for (int i = 0; i < iteration; i++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte) 0);
        }


        return clip;
    }
}
