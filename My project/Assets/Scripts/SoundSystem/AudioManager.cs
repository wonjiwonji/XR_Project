using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager() { }
    public static AudioManager Instance { get; private set; }

    // �̱���

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource buttonSource;  // AudioSource ������Ʈ���� ����

    public float soundVolume = 1.0f;
    public float bgmVolume = 1.0f;

    private bool fadeInMusicflag = false;   // ���̵� �ξƿ��� ���� flag

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
            DontDestroyOnLoad(gameObject);  // Scene�� ����ŵ� �ı����� ����
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (!clip) return;

        musicSource.clip = clip;
        musicSource.volume = bgmVolume;
        musicSource.Play(); // ���� ����ó�� �ϳ��� ��� ����Ǵ� ���� Play�� ��
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (!clip) return;

        sfxSource.clip = clip;
        sfxSource.volume = bgmVolume;
        sfxSource.PlayOneShot(sfxSource.clip);  // ������ ���� �Ҹ�
    }

    public void PlayOneShotButton(AudioClip clip)
    {
        if (!clip) return;

        buttonSource.clip = clip;
        buttonSource.volume = bgmVolume;
        buttonSource.PlayOneShot(buttonSource.clip);   // ��ư ������ �� ���� �Ҹ�
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
        // ���� ������ ���̵� �ƿ�
        yield return StartCoroutine(FadeOut(musicSource, fadeTime));

        // ���ο� ������ ���̵� ��
        musicSource.clip = newMusic;
        yield return StartCoroutine(FadeIn(musicSource, fadeTime));

        fadeInMusicflag = false;
    }
}

