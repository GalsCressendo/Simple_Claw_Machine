using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [Serializable]
    public class AudioAttribute
    {
        public string audioName;
        public AudioClip clip;
        public bool isLoop;
        public bool playOnAwake;
        public AudioSource source;
    }

    public const string BGM_AUDIO = "BGM";
    public const string CLAW1_AUDIO = "Claw1";
    public const string CLAW2_AUDIO = "Claw2";
    public const string START_AUDIO = "Start";
    public const string AMBIENCE_AUDIO = "Ambience";

    public AudioAttribute[] audioAttribute;

    private void Awake()
    {
        foreach(var attribute in audioAttribute)
        {
            AudioSource au = gameObject.AddComponent<AudioSource>();
            au.name = attribute.audioName;
            au.clip = attribute.clip;
            au.loop = attribute.isLoop;
            au.playOnAwake = attribute.playOnAwake;
            attribute.source = au;
        }
    }

    private void Start()
    {
        Play(BGM_AUDIO);
        SetVolume(BGM_AUDIO, 0.7f);

        Play(AMBIENCE_AUDIO);
        SetVolume(AMBIENCE_AUDIO, 0.7f);
    }

    public void Play(string audioName)
    {
        AudioAttribute au = Array.Find(audioAttribute, s => s.audioName == audioName);
        au.source.Play();
    }

    public void Stop(string audioName)
    {
        AudioAttribute au = Array.Find(audioAttribute, s => s.audioName == audioName);
        au.source.Stop();
    }

    public void SetVolume(string name, float value)
    {
        AudioAttribute au = Array.Find(audioAttribute, s => s.audioName == name);
        au.source.volume = value;
    }



}
