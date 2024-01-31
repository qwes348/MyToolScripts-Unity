using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableData/AudioDB", fileName = "AudioDB")]
public class AudioDB : SerializedScriptableObject
{
    [SerializeField]
    private List<AudioClipInfo> bgmClipInfos;
    [Title("")]
    [PropertySpace(SpaceBefore = 20)]
    [SerializeField]
    private List<AudioClipInfo> sfxClipInfos;
    [Title("")]
    [PropertySpace(SpaceBefore = 20)]
    [SerializeField]
    private List<AudioClipInfo> voiceClipInfos;

    public AudioClipInfo GetBgmClipInfo(string nameKey)
    {
        return bgmClipInfos.Find(clip => clip.AudioName == nameKey);
    }

    public AudioClipInfo GetSfxClipInfo(string nameKey)
    {
        return sfxClipInfos.Find(clip => clip.AudioName == nameKey);
    }
    public AudioClipInfo GetVoiceClipInfo(string nameKey)
    {
        return voiceClipInfos.Find(clip => clip.AudioName == nameKey);
    }
}
