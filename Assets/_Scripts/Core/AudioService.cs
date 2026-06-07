using UnityEngine;

public interface IAudioService
{    
    void PlaySFX(AudioClip clip);
}

public class AudioService : MonoBehaviour, IAudioService
{
    [SerializeField] private AudioSource _sfxSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        _sfxSource.pitch = Random.Range(0.9f, 1.1f);
        _sfxSource.PlayOneShot(clip);
    }
}