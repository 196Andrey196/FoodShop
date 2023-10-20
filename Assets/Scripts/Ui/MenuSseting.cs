using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSseting : MonoBehaviour
{
    [SerializeField] private Button _soundsBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _saveSettings;
    [SerializeField] private Color _onButton;
    [SerializeField] private Color _offButton;
    private AudioSource _audioSourceSounds;
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioClip _clickButton;
    private bool _buttonStatusMusic = false;
    private bool _buttonStatusSounds = false;
    void Start()
    {
        _audioSourceSounds = GetComponent<AudioSource>();
        SetSaveSettings();
        _musicBtn.onClick.AddListener(() => ButtonStatus(_musicBtn, _audioSourceMusic, ref _buttonStatusMusic));
        _soundsBtn.onClick.AddListener(() => ButtonStatus(_soundsBtn, _audioSourceSounds, ref _buttonStatusSounds));
        _saveSettings.onClick.AddListener(() => ButtonSave());
    }
    private void SetSaveSettings()
    {
        _buttonStatusMusic = PlayerPrefs.GetInt("buttonStatusMusic", 1) == 1;
        _buttonStatusSounds = PlayerPrefs.GetInt("buttonStatusSounds", 1) == 1;
        _audioSourceMusic.mute = _buttonStatusMusic;
        _audioSourceSounds.mute = _buttonStatusSounds;

        SetButtonState(_musicBtn, _buttonStatusMusic);
        SetButtonState(_soundsBtn, _buttonStatusSounds);
    }
    private void SetButtonState(Button button, bool buttonStatus)
    {
        TextMeshProUGUI buttonText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (buttonStatus)
        {
            SetButtonColor(button, _offButton);
            buttonText.text = "OFF";

        }
        else
        {
            SetButtonColor(button, _onButton);
            buttonText.text = "ON";
        }
    }

    private void ButtonStatus(Button button, AudioSource audioSource, ref bool buttonStatus)
    {
        SoundManager.instance.PlaySound(_clickButton);
        TextMeshProUGUI buttonText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (!buttonStatus)
        {
            buttonStatus = true;
            audioSource.mute = buttonStatus;
            SetButtonColor(button, _offButton);
            buttonText.text = "OFF";
        }
        else
        {
            buttonStatus = false;
            audioSource.mute = buttonStatus;
            SetButtonColor(button, _onButton);
            buttonText.text = "ON";
        }
    }

    private void ButtonSave()
    {
        _saveSettings.transform.parent.transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("buttonStatusMusic", _buttonStatusMusic ? 1 : 0);
        PlayerPrefs.SetInt("buttonStatusSounds", _buttonStatusSounds ? 1 : 0);
    }
    private void SetButtonColor(Button button, Color newColor)
    {
        Image buttonImage = button.GetComponent<Image>();
        newColor.a = 1;
        buttonImage.color = newColor;
    }

}
