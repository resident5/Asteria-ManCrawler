using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
    #endregion

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public Sound[] sounds;

    [Header("Audio Clip")]
    public AudioClip[] clickSounds;
    public AudioClip menuButton;
    public AudioClip wordPop;

    public void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayRandomSound()
    {
        int randomSound = Random.Range(0, clickSounds.Length);
        sfxSource.clip = clickSounds[randomSound];
        sfxSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume;
    public float pitch;


}

