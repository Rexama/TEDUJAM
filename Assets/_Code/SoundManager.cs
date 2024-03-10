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

    FMOD.Studio.Bus masterBus;



    void Start()
    {
        KnifeInstance = RuntimeManager.CreateInstance(KnifeEvent);
        PotStirInstance = RuntimeManager.CreateInstance(PotStirEvent);
        MortarPestleInstance = RuntimeManager.CreateInstance(MortarPestleEvent);
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");

        FMODUnity.RuntimeManager.PlayOneShot("event:/Music/GameplayMusic");
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ambience/RoomTone");
        masterBus.setVolume(0.75f);
    }

    //void Update()
    //{
    //    // Example: play the sound when the user presses a key
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        DecreaseMasterVolume();
    //    }
    //}


    int volumeState = 2;
    void SoundButtonClicked()
    {
        if(volumeState == 1)
        {
            masterBus.setVolume(0.75f);
            volumeState = 2;
        }
        else if(volumeState == 2)
        {
            masterBus.setVolume(0.5f);
            volumeState = 3;
        }
        else if (volumeState == 3)
        {
            masterBus.setVolume(0.25f);
            volumeState = 4;
        }
        else if (volumeState == 4)
        {
            masterBus.setVolume(0.0f);
            volumeState = 1;
        }
    }

    void OnDestroy()
    {
        KnifeInstance.release();
    }
}
