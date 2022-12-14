using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public enum SOUNDS { QUACK, PLAYER_ATK_1, PLAYER_ATK_2, LAST_NO_USE }

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager Instance
    {
        get { return m_instance; }
        private set { }
    }

    [SerializeField] private AudioMixer m_audioMixer;
    [SerializeField] private AudioSource m_SFXAudioSource;
    [SerializeField] private AudioSource m_BGMAudioSource;

    [Header("Master[0] Ambient[1] Effects[2]")]
    [SerializeField] private Slider[] m_volumeSliders = new Slider[3];

    [Header("SFX")]
    [SerializeField] private List<AudioClip> gameSFX;

    private void Awake()
    {
        m_instance = this;
    }
    private void Start()
    {
        if (m_volumeSliders[0] != null && m_volumeSliders[1] != null && m_volumeSliders[2] != null) { SetSliderInitialvalues(); }
    }

    private void SetSliderInitialvalues()
    {
        m_SFXAudioSource.enabled = false;
        float value;

        m_audioMixer.GetFloat("MasterVolume", out value);
        m_volumeSliders[0].value = value;

        m_audioMixer.GetFloat("BGMVolume", out value);
        m_volumeSliders[1].value = value;

        m_audioMixer.GetFloat("SFXVolume", out value);
        m_volumeSliders[2].value = value;

        m_SFXAudioSource.enabled = true;
    }

    #region Volume Setters
    public void SetMasterVolume(float value)
    {
        m_audioMixer.SetFloat("MasterVolume", value);
    }
    public void SetAmbientVolume(float value)
    {
        m_audioMixer.SetFloat("BGMVolume", value);
    }
    public void SetEffectsVolume(float value)
    {
        m_audioMixer.SetFloat("SFXVolume", value);
        if (m_SFXAudioSource.enabled && !m_SFXAudioSource.isPlaying) { m_SFXAudioSource.PlayOneShot(gameSFX[(int)SOUNDS.QUACK]); }
    }
    #endregion
    public float BGM_Volume { get { return m_BGMAudioSource.volume; } set { m_BGMAudioSource.volume = value; } }
    public AudioClip GetSound(int index) { return gameSFX[index]; }
}