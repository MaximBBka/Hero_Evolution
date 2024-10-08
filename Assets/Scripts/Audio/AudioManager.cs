using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource Music;
        public AudioSource Sound;
        public AudioClip[] ClickHero;
        public AudioClip[] Fight;
        public AudioClip[] Merge;
        public AudioClip ButtonUpgardeStrong;
        public AudioClip MusicMain;
        public AudioClip MusicBattle;
        public AudioClip Win;
        public AudioClip Lose;
        public AudioClip OpenHero;


        public static AudioManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else
            {
                Instance = this;
            }
        }
        private void Start()
        {
            float musicValue = AudioManager.Instance.Music.volume;

            if (PlayerPrefs.HasKey("Music"))
            {
                musicValue = PlayerPrefs.GetFloat("Music");
            }
            PlayerPrefs.SetFloat("Music", musicValue);
            AudioManager.Instance.Music.volume = musicValue;

            float soundValue = AudioManager.Instance.Sound.volume;
            if (PlayerPrefs.HasKey("Volume"))
            {
                soundValue = PlayerPrefs.GetFloat("Volume");
            }
            PlayerPrefs.SetFloat("Volume", soundValue);
            AudioManager.Instance.Sound.volume = soundValue;
            int chekIndex = SceneManager.GetActiveScene().buildIndex;
            if (chekIndex == 0)
            {
                Music.clip = MusicMain;
                Music.Play();
            }else
            {
                Music.clip = MusicBattle;
                Music.Play();
            }
        }
        public void ButtonSound(AudioClip clip)
        {
            AudioManager.Instance.Sound.PlayOneShot(clip);
        }
    }
}
