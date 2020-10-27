using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!AudioSource.isPlaying && GetComponent<Vehicle>().inVehicle)
        {
            AudioSource.Play();
        }
    }

    /// <summary>
    /// Plays the cars engine while driving higher speed
    /// </summary>
    public void PlayDrivingSound()
    {

        if (CarSounds.Count > 0)
        {
            AudioSource.clip = CarSounds[0];
        }
    }

    /// <summary>
    /// PLay car engine not pushing W. Idle condition
    /// </summary>
    public void PlayIdleCarSound()
    {
       
        if (CarSounds.Count > 0)
        {
                AudioSource.clip = CarSounds[2];
        }
    }

    /// <summary>
    /// Plays car using breaks
    /// </summary>
    public void PlayCarBreakSound()
    {

        if (CarSounds.Count > 0)
        {
            AudioSource.clip = CarSounds[4];
        }
    }

    /// <summary>
    /// Plays car drifting around sound
    /// </summary>
    public void PlayCarDriftingSound()
    {

        if (CarSounds.Count > 0 )
        {
              AudioSource.clip = CarSounds[3];
        }
    }

    /// <summary>
    /// Plays car accelerating
    /// </summary>
    public void PlayCarAccelerateSound()
    {
        if (CarSounds.Count > 0 )
        {
            AudioSource.clip = CarSounds[1];
        }
    }

    /// <summary>
    /// Plays cvar engine slowing down
    /// </summary>
    public void PlayCarSlowdownSound()
    {
        if (CarSounds.Count > 0)
        {
            AudioSource.clip = CarSounds[5];
        }
    }

    /// <summary>
    /// plays car door sound
    /// </summary>
    public void PlayCarDoor()
    {
        if (CarSounds.Count > 0 )
        {
            AudioSource.clip = CarSounds[6];
            AudioSource.Play();
        }
    }
}
