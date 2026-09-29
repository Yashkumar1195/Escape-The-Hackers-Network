using UnityEngine;

namespace EscapeTheHackerNetwork
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Clips")]
        public AudioClip backgroundMusic;
        public AudioClip terminalBootBeep;
        public AudioClip phishingAlertSiren;
        public AudioClip successChime;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (bgmSource != null && backgroundMusic != null)
            {
                bgmSource.clip = backgroundMusic;
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }

        public void PlayTerminalBoot()
        {
            PlaySFX(terminalBootBeep);
        }

        public void PlayAlertSiren()
        {
            PlaySFX(phishingAlertSiren);
        }

        public void PlaySuccessChime()
        {
            PlaySFX(successChime);
        }

        public void PlaySFX(AudioClip clip)
        {
            if (sfxSource != null && clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
    }
}
