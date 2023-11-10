using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public const string BUTTONS_AUDIO = "Buttons";
    public const string WIN_AUDIO = "Win";

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
        MainMenuAudio();
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

    public void MainMenuAudio()
    {
        SetVolume(BGM_AUDIO, 0.7f);

        Play(AMBIENCE_AUDIO);
        SetVolume(AMBIENCE_AUDIO, 0.7f);
    }

    public void PlayResetButtonClicked()
    {
        Play(START_AUDIO);
        SetVolume(START_AUDIO, 1);

        SetVolume(BGM_AUDIO, 1f);

        Stop(AMBIENCE_AUDIO);
    }

    public void ButtonsClicked()
    {
        Play(BUTTONS_AUDIO);
        SetVolume(START_AUDIO, 1);
    }

    public void Win()
    {
        Play(WIN_AUDIO);
        SetVolume(WIN_AUDIO,1f);
    }

    public void ClawMove1()
    {
        AudioAttribute au = Array.Find(audioAttribute, s => s.audioName == CLAW1_AUDIO);

        if (!au.source.isPlaying)
        {
            Play(CLAW1_AUDIO);
            SetVolume(CLAW1_AUDIO, 0.5f);
        }

    }

    public void ClawMove2()
    {

        AudioAttribute au = Array.Find(audioAttribute, s => s.audioName == CLAW2_AUDIO);

        if (!au.source.isPlaying)
        {
            Play(CLAW2_AUDIO);
            SetVolume(CLAW2_AUDIO, 0.5f);
        }
    }

    public void StopClawMove1()
    {
        AudioAttribute au = Array.Find(audioAttribute, s => s.audioName == CLAW1_AUDIO);

        if(au.source.isPlaying)
        {
            au.source.Stop();
        }
    }

    public void StopClawMove2()
    {
        AudioAttribute au = Array.Find(audioAttribute, s => s.audioName == CLAW2_AUDIO);

        if (au.source.isPlaying)
        {
            au.source.Stop();
        }
    }



}
