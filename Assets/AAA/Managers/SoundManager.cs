using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgMusicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Musics")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("SFX Clips")]
    [SerializeField] private List<SFXClip> sfxClips = new List<SFXClip>();

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float bgVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

    [System.Serializable]
    public class SFXClip
    {
        public string name;
        public AudioClip clip;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSFXDictionary();
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeSFXDictionary()
    {
        sfxDictionary.Clear();
        foreach (var sfx in sfxClips)
        {
            if (!sfxDictionary.ContainsKey(sfx.name))
            {
                sfxDictionary.Add(sfx.name, sfx.clip);
            }
        }
    }

    #region Background Music

    public void PlayMenuMusic()
    {
        PlayBGMusic(menuMusic);
    }

    public void PlayGameMusic()
    {
        PlayBGMusic(gameMusic);
    }

    public void PlayBGMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (bgMusicSource.clip == clip && bgMusicSource.isPlaying) return;

        bgMusicSource.clip = clip;
        bgMusicSource.loop = true;
        bgMusicSource.volume = bgVolume;
        bgMusicSource.Play();
    }

    public void StopBGMusic()
    {
        bgMusicSource.Stop();
    }

    public void PauseBGMusic()
    {
        bgMusicSource.Pause();
    }

    public void ResumeBGMusic()
    {
        bgMusicSource.UnPause();
    }

    #endregion

    #region SFX

    public void PlaySFX(string sfxName)
    {
        if (sfxDictionary.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
        else
        {
            Debug.LogWarning($"SFX bulunamadı: {sfxName}");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    #endregion

    #region Volume Controls

    public void SetBGVolume(float volume)
    {
        bgVolume = Mathf.Clamp01(volume);
        bgMusicSource.volume = bgVolume;
        SaveVolumeSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        SaveVolumeSettings();
    }

    public void ToggleBGMusic(bool isOn)
    {
        bgMusicSource.mute = !isOn;
        PlayerPrefs.SetInt("BGMusicOn", isOn ? 1 : 0);
    }

    public void ToggleSFX(bool isOn)
    {
        sfxSource.mute = !isOn;
        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("BGVolume", bgVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        bgVolume = PlayerPrefs.GetFloat("BGVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        bgMusicSource.mute = PlayerPrefs.GetInt("BGMusicOn", 1) == 0;
        sfxSource.mute = PlayerPrefs.GetInt("SFXOn", 1) == 0;

        bgMusicSource.volume = bgVolume;
    }

    #endregion
}