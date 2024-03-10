using UnityEngine;
using FMODUnity;

public class SoundManager : Singleton<SoundManager>
{
    public FMODUnity.EventReference KnifeEvent;
    public FMOD.Studio.EventInstance KnifeInstance;

    public FMODUnity.EventReference PotStirEvent;
    public FMOD.Studio.EventInstance PotStirInstance;

    public FMODUnity.EventReference MortarPestleEvent;
    public FMOD.Studio.EventInstance MortarPestleInstance;



    void Start()
    {
        KnifeInstance = RuntimeManager.CreateInstance(KnifeEvent);
        PotStirInstance = RuntimeManager.CreateInstance(PotStirEvent);
        MortarPestleInstance = RuntimeManager.CreateInstance(MortarPestleEvent);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Music/GameplayMusic");
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ambience/RoomTone");
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
        KnifeInstance.start();
    }

    void OnDestroy()
    {
        KnifeInstance.release();
    }
}
