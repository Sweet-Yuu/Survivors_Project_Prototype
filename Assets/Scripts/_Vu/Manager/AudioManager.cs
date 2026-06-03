using DG.Tweening; // Sử dụng DoTween để làm mượt âm lượng nhạc
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source Pools")]
    [SerializeField] private AudioSource interfaceSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject audioSourcePrefab;

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
        }
    }
    

    public void PlayMusic(AudioClip musicClip, float fadeDuration = 1f)
    {
        if (musicSource == null || musicClip == null) return;

        if (musicSource.isPlaying && musicSource.clip == musicClip) return;

        if (musicSource.isPlaying)
        {
            musicSource.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                musicSource.Stop();
                musicSource.clip = musicClip;
                musicSource.Play();
                musicSource.DOFade(1f, fadeDuration);
            });
        }
        else
        {
            musicSource.clip = musicClip;
            musicSource.volume = 0f;
            musicSource.Play();
            musicSource.DOFade(1f, fadeDuration);
        }
    }

    public void StopMusic(float fadeDuration = 1f)
    {
        if (musicSource == null) return;
        musicSource.DOFade(0f, fadeDuration).OnComplete(() => musicSource.Stop());
    }

    public void Play2DSound(AudioClip clip)
    {
        if (clip == null || interfaceSource == null) return;
        interfaceSource.PlayOneShot(clip);
    }

    public void Play3DSound(AudioClip clip, Vector3 position, AudioMixerGroup outputGroup = null)
    {
        if (clip == null || audioSourcePrefab == null) return;

        GameObject go = Instantiate(audioSourcePrefab);
        go.transform.position = position;

        AudioSource source = go.GetComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 1f;

        if (outputGroup != null)
        {
            source.outputAudioMixerGroup = outputGroup;
        }

        source.Play();
        Destroy(go, clip.length);
    }
}