using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string knfieSoundPath; // Set this in the inspector to the path of your FMOD event

    private FMOD.Studio.EventInstance soundEvent;

    void Start()
    {
        // Create an instance of the FMOD event
        soundEvent = RuntimeManager.CreateInstance(knfieSoundPath);
    }

    void Update()
    {
        // Example: play the sound when the user presses a key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        // Start playing the FMOD event
        soundEvent.start();
    }

    void OnDestroy()
    {
        // Release the FMOD event instance when the object is destroyed
        soundEvent.release();
    }
}
