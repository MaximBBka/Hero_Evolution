using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _sound;


        private void OnEnable()
        {
            _music.value = AudioManager.Instance.Music.volume;
            _music.onValueChanged.AddListener(SettingsMusic);
            _sound.value = AudioManager.Instance.Sound.volume;
            _sound.onValueChanged.AddListener(SettingsSound);
        }
        private void OnDisable()
        {
            _music.onValueChanged.RemoveListener(SettingsMusic);
            _sound.onValueChanged.RemoveListener(SettingsSound);
        }
        private void SettingsMusic(float value)
        {
            AudioManager.Instance.Music.volume = value;
            PlayerPrefs.SetFloat("Music", value);
        }
        private void SettingsSound(float value)
        {
            AudioManager.Instance.Sound.volume = value;
            PlayerPrefs.SetFloat("Volume", value);
        }
    }
}
