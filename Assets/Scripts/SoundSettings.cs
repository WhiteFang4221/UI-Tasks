using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _buttonsVolumeSlider;

    private float _minVolumeDb = -80;
    private float _volumeConversionNumber = 20;
    private int _VolumeOnNumber = 1;
    private int _VolumeOffNumber = 0;
    private int _decimalLogarithmNumber = 10;

    private string _masterVolumeString = "MasterVolume";
    private string _musicVolumeString = "MusicVolume";
    private string _buttonsVolumeString = "ButtonsVolume";
    private string _switchVolumeString = "IsSoundsOn";

    private bool _isSoundsOn = true;

    private void Start()
    {
        LoadSavedVolume(_masterVolumeString, _masterVolumeSlider);
        LoadSavedVolume(_musicVolumeString, _musicVolumeSlider);
        LoadSavedVolume(_buttonsVolumeString, _buttonsVolumeSlider);

        if (PlayerPrefs.HasKey(_switchVolumeString))
        {
            _isSoundsOn = PlayerPrefs.GetInt(_switchVolumeString) == _VolumeOnNumber;
            UpdateMasterVolume();
        }
    }

    public void SwitchVolume()
    {
        _isSoundsOn = !_isSoundsOn;
        UpdateMasterVolume();
        PlayerPrefs.SetInt(_switchVolumeString, _isSoundsOn ? _VolumeOnNumber : _VolumeOffNumber);
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolumeSlider.value = volume;
        SetVolume(_masterVolumeString, volume);
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolumeSlider.value = volume;
        SetVolume(_musicVolumeString, volume);
    }

    public void SetButtonsVolume(float volume)
    {
        _buttonsVolumeSlider.value = volume;
        SetVolume(_buttonsVolumeString, volume);
    }

    private void SetVolume(string volumeKey, float volume)
    {
        float dbVolume = Mathf.Log10(volume) * _volumeConversionNumber;
        _audioMixer.audioMixer.SetFloat(volumeKey, dbVolume);
        PlayerPrefs.SetFloat(volumeKey, dbVolume);
    }

    private void LoadSavedVolume(string volumeKey, Slider slider)
    {
        if (PlayerPrefs.HasKey(volumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(volumeKey);
            float normalizedVolume = Mathf.Pow(_decimalLogarithmNumber, savedVolume / _volumeConversionNumber);
            slider.value = normalizedVolume;
            SetVolume(volumeKey, slider.value);
        }
    }

    private void UpdateMasterVolume()
    {
        float dbVolume = _isSoundsOn ? Mathf.Log10(_masterVolumeSlider.value) * _volumeConversionNumber : _minVolumeDb;
        _audioMixer.audioMixer.SetFloat(_masterVolumeString, dbVolume);
    }
}
