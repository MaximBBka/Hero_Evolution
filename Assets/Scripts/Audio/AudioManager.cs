using System.Collections.Generic;
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
        public AudioClip[] MusicClips;
        public AudioClip MusicBattle;
        public AudioClip Win;
        public AudioClip Lose;
        public AudioClip OpenHero;

        private List<AudioClip> availableTracks;

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
            int checkIndex = SceneManager.GetActiveScene().buildIndex;
            if (checkIndex == 0)
            {
                StartPlaying();
            }
            else
            {
                Music.clip = MusicBattle;
                Music.Play();
            }
        }
        public void ButtonSound(AudioClip clip)
        {
            AudioManager.Instance.Sound.PlayOneShot(clip);
        }

        void StartPlaying()
        {
            availableTracks = new List<AudioClip>(MusicClips);
            PlayNextTrack();
        }

        void PlayNextTrack()
        {
            if (availableTracks.Count == 0)
            {
                availableTracks = new List<AudioClip>(MusicClips);
                ShuffleTracks(availableTracks);
            }

            int randomIndex = Random.Range(0, availableTracks.Count);
            AudioClip nextTrack = availableTracks[randomIndex];

            availableTracks.RemoveAt(randomIndex);

            Music.clip = nextTrack;
            Music.Play();

            StartCoroutine(WaitForTrackToEnd(Music.clip.length));
        }

        System.Collections.IEnumerator WaitForTrackToEnd(float trackLength)
        {
            yield return new WaitForSeconds(trackLength - 3f);
            PlayNextTrack();
        }

        void ShuffleTracks(List<AudioClip> tracksToShuffle)
        {
            for (int i = 0; i < tracksToShuffle.Count; i++)
            {
                AudioClip temp = tracksToShuffle[i];
                int randomIndex = Random.Range(i, tracksToShuffle.Count);
                tracksToShuffle[i] = tracksToShuffle[randomIndex];
                tracksToShuffle[randomIndex] = temp;
            }
        }
    }
}
