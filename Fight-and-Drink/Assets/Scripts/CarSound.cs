using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    public static CarSound Instance;

    public List<AudioClip> CarSounds;
    public AudioSource AudioSource;
    bool isCarDrivingPlaying;
    bool isCarIdlePlaying;
    bool isCarBreakPlaying;
    bool isCarDriftingPlaying;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        isCarDrivingPlaying = false;
        isCarIdlePlaying = false;
       // CarSounds = new List<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!AudioSource.isPlaying && isCarDrivingPlaying || isCarIdlePlaying || isCarDriftingPlaying)
        {
            Debug.Log("updates");
            AudioSource.Play();
        }
    }

    public void PlayDrivingSound()
    {
        if (CarSounds.Count > 0 && !isCarDrivingPlaying)
        {
            isCarBreakPlaying = false;
            isCarIdlePlaying = false;
            
        Debug.Log("DRIVING");
            AudioSource.clip = CarSounds[0];
            isCarDrivingPlaying = true;
        }
    }

    public void PlayIdleCarSound()
    {
        if (CarSounds.Count > 0 && !isCarIdlePlaying)
        {
            isCarBreakPlaying = false;
            isCarDrivingPlaying = false;
            isCarDriftingPlaying = false;
            Debug.Log("IDLE");
            isCarIdlePlaying = true;
            AudioSource.clip = CarSounds[1];
        }
    }

    public void PlayCarBreakSound()
    {

        if (CarSounds.Count > 0 && !isCarBreakPlaying)
        {
            isCarDrivingPlaying = false;
            isCarIdlePlaying = false;
            isCarDrivingPlaying = false;
            Debug.Log("BREAK");
            if (!isCarBreakPlaying)
            {
            AudioSource.clip = CarSounds[2];

                AudioSource.Play();
            }
            isCarBreakPlaying = true;
        }
    }

    public void PlayCarDriftingSound()
    {
        if (CarSounds.Count > 0 && !isCarDriftingPlaying)
        {
                isCarDrivingPlaying = false;
                isCarIdlePlaying = false;
                AudioSource.clip = CarSounds[3];
                isCarDriftingPlaying = true;
        }
    }
}
