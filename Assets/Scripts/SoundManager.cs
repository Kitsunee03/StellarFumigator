using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager Instance
    {
        get { return m_instance; }
        private set { }
    }

    [SerializeField] private AudioMixer m_audioMixer;
    [Header("Master[0] Ambient[1] Effects[2]")]
    [SerializeField] private Slider[] m_volumeSliders = new Slider[3];

    private void Start()
    {
        SetSliderInitialvalues();
    }

    private void SetSliderInitialvalues()
    {
        float value;
        m_audioMixer.GetFloat("MasterVolume", out value);
        m_volumeSliders[0].value = value;

        m_audioMixer.GetFloat("BGMVolume", out value);
        m_volumeSliders[1].value = value;

        m_audioMixer.GetFloat("SFXVolume", out value);
        m_volumeSliders[2].value = value;
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
    }
    #endregion
}