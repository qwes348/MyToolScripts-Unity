using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class SoundManager : SingletonSerializedMono<SoundManager>
{
    private AudioSource bgmAudio;
    private AudioSource sfxAudio;
    private AudioSource voiceAudio;

    public AudioClipInfo CurrentBgmClipInfo { get; private set; }

    public float BgmVolume { get => SaveLoadManager.Instance.LocalSave.BgmVolume; }
    public float SfxVolume { get => SaveLoadManager.Instance.LocalSave.SfxVolume; }
    public float VoiceVolume { get => SaveLoadManager.Instance.LocalSave.VoiceVolume; }

    private TweenerCore<float, float, FloatOptions> runningBgmFadeTween;

    private Coroutine runningSfxLoopCor;

    private void Awake()
    {
        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
            //Init();
            StartCoroutine(WaitForLoadData());
        }
        else
            Destroy(gameObject);
    }

    IEnumerator WaitForLoadData()
    {
        while (!SaveLoadManager.Instance.IsLocalDataLoadingComplete)
            yield return null;

        Init();
    }

    private void Init()
    {
        GameObject bgmObj = new GameObject("BGM");
        GameObject sfxObj = new GameObject("SFX");
        GameObject voiceObj = new GameObject("VOICE");

        bgmObj.transform.SetParent(transform);
        sfxObj.transform.SetParent(transform);
        voiceObj.transform.SetParent(transform);

        bgmAudio = bgmObj.AddComponent<AudioSource>();
        bgmAudio.loop = true;
        SetBgmPitch(1f); //bgm pitch 초기화

        sfxAudio = sfxObj.AddComponent<AudioSource>();
        voiceAudio = voiceObj.AddComponent<AudioSource>();

        UpdateVolume();

        PlayBgm(GameDB.Instance.AudioDataBase.GetBgmClipInfo("title_bgm"));
    }

    public void UpdateVolume()
    {
        bgmAudio.volume = BgmVolume;
        sfxAudio.volume = SfxVolume;
        voiceAudio.volume = VoiceVolume;
    }

    public void PlayBgm(AudioClipInfo info)
    {
        if (info == null)
            return;

        CurrentBgmClipInfo = info;
        if (bgmAudio.isPlaying)
        {
            if (runningBgmFadeTween != null && runningBgmFadeTween.IsPlaying())
            {
                runningBgmFadeTween.Kill();
            }

            runningBgmFadeTween = bgmAudio.DOFade(0f, 0.5f).OnComplete(() =>
                                    {
                                        bgmAudio.clip = info.GetClip();
                                        bgmAudio.Play();

                                        runningBgmFadeTween = bgmAudio.DOFade(BgmVolume, 0.5f);
                                    });
        }
        else
        {
            bgmAudio.clip = info.GetClip();
            bgmAudio.Play();
        }
    }

    public void PlayBgm(string keyName)
    {
        var clipInfo = GameDB.Instance.AudioDataBase.GetBgmClipInfo(keyName);
        if (clipInfo == null)
            return;

        PlayBgm(clipInfo);
    }

    public void StopBgm()
    {
        bgmAudio.Stop();
    }

    public void PlaySfx(AudioClipInfo info)
    {
        if (info == null)
            return;

        sfxAudio.PlayOneShot(info.GetClip());
    }

    public void PlaySfx(string keyName)
    {
        var clipInfo = GameDB.Instance.AudioDataBase.GetSfxClipInfo(keyName);
        if (clipInfo == null)
            return;

        PlaySfx(clipInfo);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    public void PlaySfxLoop(string keyName, int loopCount, float interval)
    {
        var clipInfo = GameDB.Instance.AudioDataBase.GetSfxClipInfo(keyName);
        if (clipInfo == null)
            return;
        runningSfxLoopCor = StartCoroutine(PlaySfxLoopCor(clipInfo, loopCount, interval));
    }

    IEnumerator PlaySfxLoopCor(AudioClipInfo clipInfo, int loopCount, float interval)
    {
        int count = 0;
        while (count < loopCount)
        {
            PlaySfx(clipInfo);
            count++;

            yield return new WaitForSeconds(interval);
        }
    }

    public void StopRunningSfxLoop()
    {
        if (runningSfxLoopCor == null)
            return;

        StopCoroutine(runningSfxLoopCor);
    }

    public void PlayVoice(AudioClipInfo info)
    {
        if (info == null)
            return;

        voiceAudio.PlayOneShot(info.GetClip());
    }

    public void PlayVoice(string keyName)
    {
        var clipInfo = GameDB.Instance.AudioDataBase.GetVoiceClipInfo(keyName);
        if (clipInfo == null)
            return;

        PlayVoice(clipInfo);
    }

    public float GetBgmPitch()
    {
        return bgmAudio.pitch;
    }

    public void SetBgmPitch(float pitch)
    {
        bgmAudio.pitch = pitch;
    }
}
