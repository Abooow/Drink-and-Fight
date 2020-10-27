using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ChannalChange(RadioChannel channel);

public class Radio : MonoBehaviour
{
    public static Radio Instance;

    public AudioSource AudioSource;
    public List<RadioChannel> Channels;
    public event ChannalChange OnSongResumed;

    public bool IsPlaying { get; private set; }

    public RadioChannel CurrentChannel { get; private set; }
    private int currentChannelIndex;

    void Start()
    {
        Instance = this;

        if (Channels == null) Channels = new List<RadioChannel>();
        IsPlaying = false;

        //Channels.Insert(0, RadioChannel.NoChannel);
        if (Channels.Count > 0) CurrentChannel = Channels[0];
        currentChannelIndex = 0;

        foreach (RadioChannel channel in Channels)
        {
            channel.AudioSource = AudioSource;
            channel.Reset();
        }
    }

    public void AddChannel(RadioChannel channel) { Channels.Add(channel); }

    public void ResetCurrentChannel()
    {
        CurrentChannel?.Reset();
        Resume();
    }

    public void PlayChannel(int index)
    {
        if (Channels.Count == 0) return;

        int i = index % (Channels.Count == 0 ? 1 : Channels.Count);
        if (i < 0) i += Channels.Count;

        Pause();

        currentChannelIndex = i;
        CurrentChannel = Channels[currentChannelIndex];

        Resume();
    }

    public void NextChannel() { PlayChannel(currentChannelIndex + 1); }

    public void PreviousChannel() { PlayChannel(currentChannelIndex - 1); }

    public void Resume()
    {
        IsPlaying = true;
        if (OnSongResumed != null) OnSongResumed.Invoke(CurrentChannel);

        StartCoroutine("ShowChannelLogo");
        CurrentChannel?.Resume();
        AudioSource.Play();
    }

    private void Update()
    {
        if (IsPlaying) CurrentChannel?.Update();
    }

    public void Pause()
    {
        if (CurrentChannel != null && CurrentChannel.ImageLogo != null)
            CurrentChannel.ImageLogo.SetActive(false);
        StopCoroutine("ShowChannelLogo");

        AudioSource.Stop();
        IsPlaying = false;
    }

    private static Color alphaColor = new Color(1f, 1f, 1f, 0.8f);
    IEnumerator ShowChannelLogo()
    {
        if (CurrentChannel?.ImageLogo == null) yield break;
        Image image = CurrentChannel.ImageLogo.GetComponent<Image>();
        if (image == null) yield break;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        CurrentChannel.ImageLogo.SetActive(true);
        yield return new WaitForSeconds(2);

        while (image.color.a > 0.02f)
        {
            yield return new WaitForSeconds(0.07f);
            image.color *= alphaColor;
        }
        CurrentChannel.ImageLogo.SetActive(false);
    }
}

[Serializable]
public class RadioChannel
{
    public static RadioChannel NoChannel => new RadioChannel("None", null);
    private static int totalRadioChannelsCreated;

    [HideInInspector] public AudioSource AudioSource;
    public string Name;
    public List<AudioClip> Songs;
    public GameObject ImageLogo;

    private float totalSongsLength;
    private DateTime timeLastPlayed;
    private int currenSongIndex;

    public RadioChannel(string name, List<AudioClip> songs)
    {
        totalRadioChannelsCreated++;
        if (string.IsNullOrEmpty(name)) name = $"Channel {totalRadioChannelsCreated}";

        Name = name;
        Songs = songs;

        if (Songs == null) Songs = new List<AudioClip>();
        AudioSource = new AudioSource();

        Reset();
    }

    public RadioChannel()
    {
        totalRadioChannelsCreated++;
        Reset();
    }

    public void Resume()
    {
        float resA = GetResumeAmount();
        float totalTimePlayed = resA % totalSongsLength;
        if (Songs.Count == 0) AudioSource.clip = null;
        for (int i = 0; i < Songs.Count; i++)
        {
            if (totalTimePlayed - Songs[i].length <= 0)
            {
                currenSongIndex = i;
                AudioSource.clip = Songs[i];
                AudioSource.time = totalTimePlayed;
                break;
            }
            totalTimePlayed -= Songs[i].length;
        }
    }

    public void Update()
    {
        if (!AudioSource.isPlaying && Songs.Count > 0)
        {
            currenSongIndex++;
            if (currenSongIndex >= Songs.Count)
                currenSongIndex = 0;

            Debug.Log("Changed radio song to " + currenSongIndex);
            AudioSource.clip = Songs[currenSongIndex];
            AudioSource.time = 0;
            AudioSource.Play();
        }
    }

    public void Reset()
    {
        //timeLastPlayed = DateTime.Now;
        CalculateSongsLength();
    }

    private float GetResumeAmount()
    {
        TimeSpan span = DateTime.Now - DateTime.Today;

        return (float)span.TotalSeconds;
    }

    private void CalculateSongsLength()
    {
        if (Songs == null) return;

        totalSongsLength = 0;
        for (int i = 0; i < Songs.Count; i++)
            totalSongsLength += Songs[i].length;
    }
}