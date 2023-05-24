using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    // Makes script into a singleton
    public static AudioManager instance;

    [Header("Audio clips")]
    // Arrays to store the audio clips
    public Sound[] music;
    public Sound[] sFXs;

    // Variables which will store the sliders
    [SerializeField] Slider sliderValueSFX = null;
    [SerializeField] Slider sliderValueMusic = null;

    // Variables which will store the mixers
    public AudioMixer sFXAudioMixer;
    public AudioMixer musicAudioMixer;
    public AudioMixer masterMixer;

    // Variables to store the current music volume
    float _SFXVol;
    float _MusicVolume;

    // Awake is called before Start
    void Awake()
    {
        // Stops the GameObject being destroyed,and makes sure there is only one in existance
        if (instance == null)
        {
            // if instance is null, store a reference to this instance
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("dont destroy");
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one
            print("do destroy");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Loads the value for the SFX volume
        /*
        if (!PlayerPrefs.HasKey("SFXMixer"))
        {
            PlayerPrefs.SetFloat("SFXMixer", -20f);
            LoadSFX();
        }
        else
        {
            LoadSFX();
        }
        */

        // Loads the value for the music volume
        if (!PlayerPrefs.HasKey("MusicMixer"))
        {
            PlayerPrefs.SetFloat("MusicMixer", -20f);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }

        // Loads the music clips
        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.musicVolume;
        }

        // Loads the SFX clips
        foreach (Sound s in sFXs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.sFXVolume;
        }
    }

    // Plays the selected music if the name given is correct
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null)
        {
            print("Invalid clip name" + name);
            return;
        }

        s.source.Play();
    }

    // Plays the selected SFX if the name given is correct
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sFXs, sound => sound.name == name);
        if (s == null)
        {
            print("Invalid clip name" + name);
            return;
        }

        s.source.Play();
    }

    //-----------
    // SFX audio
    //-----------
    #region SFX
    /*
    // Sets the saved music volume as the current one
    public void SetSFXVolume(float sFXvolume)
    {
        sFXAudioMixer.SetFloat("SFXMixer", sFXvolume);
        PlayerPrefs.SetFloat("SFXMixer", sFXvolume);
    }

    // Is used when slider is changed to activate the SaveSFX(); method
    public void ChangeSFXVolume()
    {
        AudioListener.volume = sliderValueSFX.value;
        SaveSFX();
    }

    // Load the saved value for the SFX volume
    private void LoadSFX()
    {
        _SFXVol = PlayerPrefs.GetFloat("SFXMixer");
        sliderValueSFX.value = _SFXVol;
        SetSFXVolume(_SFXVol);
    }

    // Saves the current SFX volume
    public void SaveSFX()
    {
        PlayerPrefs.SetFloat("SFXMixer", sliderValueSFX.value);
    }
    */
    #endregion
    //-----------
    // Music audio
    //-----------
    #region Music
    // Sets the saved music volume as the current one
    public void SetMusicVolume(float musicvolume)
    {
        musicAudioMixer.SetFloat("MusicMixer", musicvolume);
        PlayerPrefs.SetFloat("MusicMixer", musicvolume);
    }

    // Is used when slider is changed to activate the SaveMusic(); method
    public void ChangeMusicVolume()
    {
        AudioListener.volume = sliderValueMusic.value;
        SaveMusic();
    }

    // Load the saved value for the music volume
    private void LoadMusic()
    {
        _MusicVolume = PlayerPrefs.GetFloat("MusicMixer");
        sliderValueMusic.value = _MusicVolume;
        SetMusicVolume(_MusicVolume);
    }

    // Saves the current music volume
    public void SaveMusic()
    {
        PlayerPrefs.SetFloat("SFXMixer", sliderValueSFX.value);
    }
    #endregion
}