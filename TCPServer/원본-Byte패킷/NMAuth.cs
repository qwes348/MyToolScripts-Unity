using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Diagnostics;
using NGEL;
using Steamworks;
using Lean.Localization;

public enum EAuthType
{
    None = 0,
    GuestAuth = 10,
    GoogleAuth = 20,
    AppleAuth = 30,
    FacebookAuth = 40,
    VR = 100,
}

[Serializable]
public class SaveAuth
{
    public EAuthType authType = EAuthType.None;
    public string uuid;
}

[Serializable]
public class SaveAuthList
{
    public EAuthType currentAuthType = EAuthType.None;
    public List<SaveAuth> authInfo = new List<SaveAuth>();
    public int skipScenario = 0;
    public int resolutionType = 1;
    public int language = 0;

    public float _volumBM = 0.7f;
    public float volumeBM
    {
        get
        {
            return NMAuth.MathRoundFloat(_volumBM);
        }
        set
        {
            _volumBM = NMAuth.MathRoundFloat(value);
        }
    }
    public float _volumeSE = 0.7f;
    public float volumeSE
    {
        get
        {
            return NMAuth.MathRoundFloat(_volumeSE);
        }
        set
        {
            _volumeSE = NMAuth.MathRoundFloat(value);
        }
    }
    public float _volumeV = 0.7f;
    public float volumeV
    {
        get
        {
            return NMAuth.MathRoundFloat(_volumeV);
        }
        set
        {
            _volumeV = NMAuth.MathRoundFloat(value);
        }
    }
    public bool muteBM = false;
    public bool muteSE = false;
    public bool muteV = false;
    public bool isControllerRight = true;
}

public static class NMAuth
{
    static Dictionary<EAuthType, SaveAuth> authInfo = new Dictionary<EAuthType, SaveAuth>();
    static public SaveAuth currentAuth;
    static public SaveAuthList saveDataList = null;

    static public void LoadData()
    {
        string path = Application.persistentDataPath + "/jamong";
        DirectoryInfo di = new DirectoryInfo(path);

        if (di.Exists == false)
        {
            di.Create();
            SetLanguageFirstTime();
        }
        else
        {
            if (File.Exists(path + "/auth.json"))
            {
                string _saveData = File.ReadAllText(path + "/auth.json");
                var saveData = JsonUtility.FromJson<SaveAuthList>(_saveData);
                authInfo.Clear();
                foreach (var item in saveData.authInfo)
                {
                    authInfo.Add(item.authType, item);

#if UNITY_EDITOR
                    //설정안했을때 none이여도 실행
                    if (item.authType == EAuthType.None)
                    {
                        currentAuth = item;
                    }
                    //vr로 설정했을때 덮어씀
                    if (item.authType == EAuthType.VR)
                    {
                        currentAuth = item;
                    }
#else
                    if (saveData.currentAuthType == item.authType)
                    {
                        currentAuth = item;
                    }
#endif
                }

                volumeBM = saveData.volumeBM;
                volumeSE = saveData.volumeSE;
                volumeV = saveData.volumeV;
                muteBM = saveData.muteBM;
                muteSE = saveData.muteSE;
                muteV = saveData.muteV;

                language = saveData.language;
                NGNetGameServer.Ins.language = (NGNetGameServer.Language)language;

                isControllerRight = saveData.isControllerRight;
                Valve.VR.InteractionSystem.Player.instance.GetComponent<VRAreaControl>().ChangeControllerToRight(isControllerRight);

                saveDataList = saveData;
                SoundController.instance.VolumeSetting(volumeSE);
            }
            else
            {
                SetLanguageFirstTime();
            }
        }
    }

    static public Action onSaveData;
    static public void SaveData()
    {
        SaveAuthList saveData = new SaveAuthList();
        if (currentAuth == null)
        {
            saveData.currentAuthType = EAuthType.None;
        }
        else
        {
            saveData.currentAuthType = currentAuth.authType;
        }
        
        foreach (var item in authInfo)
        {
            saveData.authInfo.Add(item.Value);
        }

        saveData.volumeBM = volumeBM;
        saveData.volumeSE = volumeSE;
        saveData.volumeV = volumeV;
        saveData.muteBM = muteBM;
        saveData.muteSE = muteSE;
        saveData.muteV = muteV;

        saveData.language = language;
        if (NGNetGameServer.instanceExists)
            NGNetGameServer.Ins.language = (NGNetGameServer.Language)language;

        saveData.isControllerRight = isControllerRight;
        Valve.VR.InteractionSystem.Player.instance.GetComponent<VRAreaControl>().ChangeControllerToRight(isControllerRight);
        saveDataList = saveData;

        //onSaveData?.Invoke();
        if (NewMenuManager.instance != null)
            NewMenuManager.instance.SettingBGM();

        SoundController.instance.VolumeSetting(muteSE ? 0 : volumeSE);

        string _saveData = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/jamong/auth.json", _saveData);
    }

    static public string GetID(EAuthType _auth)
    {
        SaveAuth ret = null;
        switch (_auth)
        {
            case EAuthType.None:
                if (false == authInfo.TryGetValue(_auth, out ret))
                    return "";

                return ret.uuid;
            case EAuthType.GuestAuth:
                if (false == authInfo.TryGetValue(_auth, out ret))
                    return "";

                return ret.uuid;
            //case EAuthType.GoogleAuth:
            //    return Social.localUser.id;
            //case EAuthType.AppleAuth:
            //    return Social.localUser.id;
            default:
                break;
        }

        if (false == authInfo.TryGetValue(_auth, out ret))
            return "";

        return ret.uuid;
    }
    static public string GetID()
    {
        if (currentAuth == null)
        {
            return "";
        }
        
        if (SteamManager.Initialized)
        {
            return SteamUser.GetSteamID().ToString();
        }

        return GetID(currentAuth.authType);
    }

    static public EAuthType GetAuthType()
    {
        if (currentAuth == null)
            return EAuthType.None;

        return currentAuth.authType;
    }
    static public void SetVRUserId(string _testID)
    {
        authInfo[EAuthType.VR] = new SaveAuth();
        authInfo[EAuthType.VR].uuid = _testID;
        authInfo[EAuthType.VR].authType = EAuthType.VR;
    }
    static public void SetCurrentAuth(EAuthType _type)
    {
        currentAuth = authInfo[_type];
    }
    static public void SetAccountAck(List<NGAccountInfo> _accountInfo)
    {
        authInfo.Clear();

        for (int i = 0; i < _accountInfo.Count; i++)
        {
            if ((int)EAuthType.None == _accountInfo[i].AuthType
                || (int)EAuthType.GuestAuth == _accountInfo[i].AuthType
                || (int)EAuthType.VR == _accountInfo[i].AuthType)
            {
                authInfo[(EAuthType)_accountInfo[i].AuthType] = new SaveAuth
                {
                    authType = (EAuthType)_accountInfo[i].AuthType,
                    uuid = _accountInfo[i].DeviceID
                };
            }
            else
            {
                authInfo[(EAuthType)_accountInfo[i].AuthType] = new SaveAuth
                {
                    authType = (EAuthType)_accountInfo[i].AuthType, 
                };
            }
        }

        SaveData();
    }

    #region VolumeSetting
    //데이터에 저장하기 전에만 쓰고 저장된거 불러올땐 saveDataList.volumeBM 등으로 사용
    static private float _volumBM = 0.7f;
    static public float volumeBM
    {
        get
        {
            return MathRoundFloat(_volumBM);
        }
        set
        {
            _volumBM = MathRoundFloat(value);
        }
    }
    static public float _volumeSE = 0.7f;
    static public float volumeSE
    {
        get
        {
            return MathRoundFloat(_volumeSE);
        }
        set
        {
            _volumeSE = MathRoundFloat(value);
        }
    }
    static public float _volumeV = 0.7f;
    static public float volumeV
    {
        get
        {
            return MathRoundFloat(_volumeV);
        }
        set
        {
            _volumeV = MathRoundFloat(value);
        }
    }

    static public bool muteBM = false;
    static public bool muteSE = false;
    static public bool muteV = false;

    static public float MathRoundFloat(float _f)
    {
        float result = 0;
        result = (float)Math.Round(_f, 2);

        return result;
    }
    static public int MathRoundInt(float _f)
    {
        int result = 0;
        result = Mathf.Clamp(Mathf.RoundToInt(_f * 100), 0, 100);

        return result;
    }
    #endregion VolumeSetting
    #region language
    static public int language = 1;
    static public void SetLanguageFirstTime()
    {
        SystemLanguage systemLanguage = Application.systemLanguage;

        if (systemLanguage == SystemLanguage.Korean)
            language = 0;
        else
            language = 1;
    }
    #endregion language
    #region Controller
    static public bool isControllerRight = true;
    #endregion Controller
}
