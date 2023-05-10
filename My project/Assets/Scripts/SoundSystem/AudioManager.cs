using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager() { }
    public static AudioManager Instance { get; private set; }

    // 싱글톤

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource buttonSource;  // AudioSource 오브젝트에서 선언

    public float soundVolume = 1.0f;
    public float bgmVolume = 1.0f;

    private bool fadeInMusicflag = false;   // 페이드 인아웃을 위한 flag

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Scene이 변경돼도 파괴되지 않음
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (!clip) return;

        musicSource.clip = clip;
        musicSource.volume = bgmVolume;
        musicSource.Play(); // 보통 뮤직처럼 하나로 계속 재생되는 것을 Play로 씀
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (!clip) return;

        sfxSource.clip = clip;
        sfxSource.volume = bgmVolume;
        sfxSource.PlayOneShot(sfxSource.clip);  // 여러번 나는 소리
    }

    public void PlayOneShotButton(AudioClip clip)
    {
        if (!clip) return;

        buttonSource.clip = clip;
        buttonSource.volume = bgmVolume;
        buttonSource.PlayOneShot(buttonSource.clip);   // 버튼 눌렀을 때 나는 소리
    }

    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = 0.0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        while (audioSource.volume < bgmVolume)
        {
            audioSource.volume += bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    public void FadeInMusic(AudioClip newMusic, float fadeTime)
    {
        if (!newMusic) return;
        if (fadeInMusicflag) return;

        StartCoroutine(FadeInMusicroutine(newMusic, fadeTime));
    }

    public IEnumerator FadeInMusicroutine(AudioClip newMusic, float fadeTime)
   {
        fadeInMusicflag = true;
        // 이전 음악을 페이드 아웃
        yield return StartCoroutine(FadeOut(musicSource, fadeTime));

        // 새로운 음악을 페이드 인
        musicSource.clip = newMusic;
        yield return StartCoroutine(FadeIn(musicSource, fadeTime));

        fadeInMusicflag = false;
    }
}

