using UnityEngine;

public class AudioManger : Singleton<AudioManger>
{
    public AudioClip[] music;
    public AudioClip[] sound;

    public float musicVolume = 1f;
    public bool musicLoop = true;
    
    public float soundVolume = 0.5f;
    
    private AudioSource _audioSource;
    protected override void Awakes()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayMusic(int num)
    {
        _audioSource.clip = num >= music.Length ? music[music.Length- 1] : music[num];
        _audioSource.Play();
        _audioSource.loop = musicLoop;
        _audioSource.volume = musicVolume;
    }
    public void PlaySound(int num)
    {
        AudioClip clip = num >= sound.Length ? sound[sound.Length - 1] : sound[num];
        AudioSource sounds = gameObject.AddComponent<AudioSource>();
        sounds.clip = clip;
        sounds.Play();
        sounds.loop = false;
        sounds.volume = soundVolume;
        Destroy(sounds, clip.length);
    }
}
