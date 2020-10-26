using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    public static CarSound Instance;

    public List<AudioClip> CarSounds;
    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
       // CarSounds = new List<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!AudioSource.isPlaying && GetComponent<Vehicle>().inVehicle)
        {
            AudioSource.Play();
        }
    }

    public void PlayDrivingSound()
    {
        if (CarSounds.Count > 0 )
        {
            AudioSource.clip = CarSounds[0];
        }
    }

    public void PlayIdleCarSound()
    {

        if (CarSounds.Count > 0 )
        {
            AudioSource.clip = CarSounds[2];
        }
    }

    public void PlayCarBreakSound()
    {

        if (CarSounds.Count > 0 )
        {
            AudioSource.clip = CarSounds[4];
        }
    }

    public void PlayCarDriftingSound()
    {

        if (CarSounds.Count > 0 )
        {
              AudioSource.clip = CarSounds[3];
        }
    }

    public void PlayCarAccelerateSound()
    {
        if (CarSounds.Count > 0 )
        {
            AudioSource.clip = CarSounds[1];
        }
    }

    public void PlayCarSlowdownSound()
    {
        if (CarSounds.Count > 0)
        {
            AudioSource.clip = CarSounds[5];
        }
    }

    public void PlayCarOpenDoor()
    {
        if (CarSounds.Count > 0)
        {
            AudioSource.clip = CarSounds[6];
        }
    }
}
