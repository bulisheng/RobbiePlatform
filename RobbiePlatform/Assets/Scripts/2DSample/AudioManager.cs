using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;
    [Header("环境声音")]
    public AudioClip ammbientClip;
    public AudioClip musicClip;

    [Header("FX 音效")]
    public AudioClip deathFXCClip;

    [Header("Robbie 音效")]
    public AudioClip[] walkStepClips;
    public AudioClip[] crounchStepClips;
    public AudioClip jumpClip;
    public AudioClip deathClip;

    public AudioClip jumpViodeClip;
    public AudioClip deathViodeClip;

    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource fxSource;
    AudioSource playerSource;
    AudioSource voiceSource;
    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(gameObject);

        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }
    void StartLevelAudio()
    {
        current.ambientSource.clip = current.ammbientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
    }
    public static void PlayFootStepAudio()
    {
        int index = Random.Range(0, current.walkStepClips.Length);
        current.playerSource.clip = current.walkStepClips[index];
        current.playerSource.Play();
    }
    public static void PlayCrouchStepAudio()
    {
        int index = Random.Range(0, current.crounchStepClips.Length);
        current.playerSource.clip = current.crounchStepClips[index];
        current.playerSource.Play();
    }
    public static void PlayJumpAudio()
    {
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();

        current.voiceSource.clip = current.jumpViodeClip;
        current.voiceSource.Play();
    }
    public static void PlayDeathAudio()
    {
        current.playerSource.clip = current.deathClip;
        current.playerSource.Play();

        current.voiceSource.clip = current.deathViodeClip;
        current.voiceSource.Play();

        current.fxSource.clip = current.deathFXCClip;
        current.fxSource.Play();
    }
}

