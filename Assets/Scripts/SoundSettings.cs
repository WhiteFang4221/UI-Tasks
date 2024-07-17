using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _buttonsVolumeSlider;

    private string _masterVolumeString = "MasterVolume";
    private string _musicVolumeString = "MusicVolume";
    private string _buttonsVolumeString = "ButtonsVolume";
    private string _switchVolumeString = "IsSoundsOn";

    private bool _isSoundsOn = true;

    private void Start()
    {
        if (PlayerPrefs.HasKey(_masterVolumeString))
        {
            float savedVolume = PlayerPrefs.GetFloat(_masterVolumeString);
            float normalizedVolume = Mathf.Pow(10, savedVolume / 20f); ;
            _masterVolumeSlider.value = normalizedVolume;
        }

        if (PlayerPrefs.HasKey(_musicVolumeString))
        {
            float savedVolume = PlayerPrefs.GetFloat(_musicVolumeString);
            float normalizedVolume = Mathf.Pow(10, savedVolume / 20f); ;
            _musicVolumeSlider.value = normalizedVolume;
        }

        if (PlayerPrefs.HasKey(_buttonsVolumeString))
        {
            float savedVolume = PlayerPrefs.GetFloat(_buttonsVolumeString);
            float normalizedVolume = Mathf.Pow(10, savedVolume / 20f); ;
            _buttonsVolumeSlider.value = normalizedVolume;
        }

        if (PlayerPrefs.HasKey(_switchVolumeString))
        {
            _isSoundsOn = PlayerPrefs.GetInt(_switchVolumeString) == 1;
            _audioMixer.audioMixer.SetFloat(_masterVolumeString, _isSoundsOn ? 0 : -80);
        }
    }
    public void SwitchVolume()
    {
        if (_isSoundsOn == true)
        {
            _audioMixer.audioMixer.SetFloat(_masterVolumeString, -80);
            _isSoundsOn = false;
        }
        else
        {
            float dbVolume = Mathf.Log10(_masterVolumeSlider.value) * 20;
            _audioMixer.audioMixer.SetFloat(_masterVolumeString, dbVolume);
            _isSoundsOn = true;
        }

        PlayerPrefs.SetInt(_switchVolumeString, _isSoundsOn ? 1 : 0);
    }

    public void SetMasterVolume(float volume)
    {
        if (_isSoundsOn == true)
        {
            float dbVolume = Mathf.Log10(volume) * 20;
            _audioMixer.audioMixer.SetFloat(_masterVolumeString, dbVolume);
            PlayerPrefs.SetFloat(_masterVolumeString, dbVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        float dbVolume = Mathf.Log10(volume) * 20;
        _audioMixer.audioMixer.SetFloat(_musicVolumeString, dbVolume);
        PlayerPrefs.SetFloat(_musicVolumeString, dbVolume);
    }

    public void SetButtonsVolume(float volume)
    {
        float dbVolume = Mathf.Log10(volume) * 20;
        _audioMixer.audioMixer.SetFloat(_buttonsVolumeString, dbVolume);
        PlayerPrefs.SetFloat(_buttonsVolumeString, dbVolume);
    }
}
