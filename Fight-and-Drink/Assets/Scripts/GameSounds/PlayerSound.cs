using Random = System.Random;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    CharacterMover cm;
    AudioSource[] audios;
    AudioSource Walk;
    AudioSource Run;
    AudioSource Standing;
    Random rnd;
    
    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<CharacterMover>();
        audios = GetComponents<AudioSource>();
        rnd = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        GetSounds();
        UpdateSound();
    }

    void GetSounds()
    {
        Walk = audios[0];
        Run = audios[1];
        Standing = audios[2];
    }
    void UpdateSound()
    {
        if (cm.MovingState == MovingState.Walking && Walk.isPlaying == false) { Walk.Play(); }
        else if (cm.MovingState == MovingState.Running && Run.isPlaying == false) { Run.Play(); }
        else if (cm.MovingState == MovingState.Standing && Standing.isPlaying == false) { Standing.PlayDelayed((float)rnd.Next(2, 25)); }
        else if (cm.MovingState != MovingState.Walking) { Walk.Pause(); }
        else if (cm.MovingState != MovingState.Running) { Run.Pause(); }
        else if (cm.MovingState != MovingState.Standing) { Standing.Pause(); }
    }
}
