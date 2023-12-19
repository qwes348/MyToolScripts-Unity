using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;

using NGNet;
using System.Linq;
using NGEL;

enum NGDeviceType
{
    ALL = -1,
    IOS = 10,
    AOS = 20,
};

public class NGNetGameServer : Singleton2<NGNetGameServer>
{
    private CNetClient client;
    public GameServerC2S.Proxy proxy = new GameServerC2S.Proxy();
    private GameServerS2C.Stub stub = new GameServerS2C.Stub();

    public string hostname;
    public int port;

    public bool isConnectServer = false;

    public Action<CNetClient> ActionOnConnect;

    bool centerServerConnect = false;
    bool userKick = false;

    NGNetCenterServer centerServer;
    public void InitCenterServer()
    {
        if (centerServer)
        {
            Destroy(centerServer.gameObject);
            centerServerConnect = false;
            centerServer = null;
            centerServerInit = false;
        }
    }

    bool centerServerInit = false;

    bool initGameServer = false;

    bool logInReq = false;


    public DateTime checkServerInfoTime;
    public DateTime checkReconnectionServerTime;

    public void ConnectCenterServer(string centerServerIP, int centerServerPort)
    {
        if (centerServer == null)
        {
            centerServerConnect = true;
            var centerServerGameObject = new GameObject();
            centerServer = centerServerGameObject.AddComponent<NGNetCenterServer>();
        }

        //센터서버 연결됬을때
        centerServer.ActionGetConnectGameServerInfoAck += (_2) =>
        {
            _2.Disconnect();
            isConnectServer = false;
            client = new CNetClient();
            proxy = new GameServerC2S.Proxy();
            stub = new GameServerS2C.Stub();
            Connect();
            centerServerInit = true;
        };
        centerServer.gameObject.SetActive(true);
        centerServer.Connect(centerServerIP, centerServerPort);
    }

    void ProgressReq()
    {
        if (logInReq)
        {
            logInReq = false;
            proxy.LogInReq(NGNet.RmiContext.Reliable, (int)NMUserInfo.Ins.AccountInfo.AccountID, (int)NMAuth.GetAuthType());
            return;
        }
    }


    private void Update()
    {
        if (userKick)
        {
            return;
        }

        if (client != null)
        {

            if (centerServerInit)
            {
                InitCenterServer();
            }
            else
            {
                if (client.GetConnectedState() == IsConnectedState.Reconnect)
                {
                }
                else if (client.IsConnected())
                {
                    ProgressReq();
                }
                try
                {
                    client.FrameMove();
                }
                catch (Exception e)
                {
                    Debug.Log("NGNetGameServer " + client.GetConnectedState().ToString() + ", " + e.Message);
                }
            }
        }
    }

    public void Connect()
    {
        if (client == null)
        {
            client = new CNetClient();
        }

        if (client.IsConnected() == false)
        {
            isConnectServer = false;
            CNetConnectionParam param = new CNetConnectionParam();
            param.hostname = hostname;
            param.port = port;
            param.OnJoinServerComplete = OnJoinServerComplete;
            //param.OnPacketReceive = OnPacketReceive;
            param.OnLeaveServer = OnLeaveServer;

            client.AttachProxy(proxy);
            client.AttachStub(stub);

            stub.LogInAck = LogInAck;
            stub.StartMatchAck = StartMatchAck;
            stub.StartMatchNot = StartMatchNot;
            stub.ChangeNicknameAck = ChangeNicknameAck;
            stub.StartChallengeModeAck = StartChallengeModeAck;
            stub.EndChallengeModeAck = EndChallengeModeAck;
            stub.EndEndlessModeAck = EndEndlessModeAck;
            stub.GetChallengeTopRankgerAck = GetChallengeTopRankgerAck;
            stub.GetEndLessModeTopRankerAck = GetEndlessTopRankgerAck;
            stub.LearnSkillAck = LearnSkillAck;
            stub.DelSkillAck = DelSkillAck;

            client.Connect(param);
        }

    }

    public void DisconnectBackground()
    {
        if (client != null)
        {
            client.Disconnect();
            isConnectServer = false;
        }
    }

    void OnDestroy()
    {
        if (client != null)
        {
            client.Disconnect();
            isConnectServer = false;
        }
    }


    public void OnJoinServerComplete(ErrorType errorType)
    {
        if (errorType == ErrorType.ErrorType_Ok && isConnectServer == false)
        {
            isConnectServer = true;
            logInReq = true;
        }
        else
        {
            Debug.LogError(errorType + "JoinServerError");
        }
    }

    public void OnLeaveServer(ErrorType errorType)
    {
        switch (errorType)
        {
            case ErrorType.ErrorType_DisconnectFromRemote:
                {
                    if (client != null)
                    {
                        isConnectServer = false;
                    }
                }
                break;
            case ErrorType.ErrorType_ReconnectFail:
                {
                    // 서버와 접속이 끈겼는데 재접속을 1분동안 못한 상태로 오는 메세지
                }
                break;
        }
    }

    private bool LogInAck(int Error, NGLogInAck accountInfo)
    {
        if (Error < 0)
        {
            Debug.LogError("LogInAck:" + Error);
            return true;
        }

        NMUserInfo.Ins.loginInfo = accountInfo;
        if (NMUserInfo.Ins.currentSkillInfo == null)
            NMUserInfo.Ins.currentSkillInfo = accountInfo.vecSkillInfo;
        if (NMUserInfo.Ins.currentCollectionInfo == null)
            NMUserInfo.Ins.currentCollectionInfo = accountInfo.vecCollectionInfo;
        if (NMUserInfo.Ins.currentProfileInfo == null)
            NMUserInfo.Ins.currentProfileInfo = accountInfo.profileInfo;

        //Debug.LogError("-------------------------------------------------");
        //Debug.LogError("iAcquiredEpicTower:" + accountInfo.profileInfo.iAcquiredEpicTower);
        //Debug.LogError("iAcquiredGold:" + accountInfo.profileInfo.iAcquiredGold);
        //Debug.LogError("iAcquiredTrohpy:" + accountInfo.profileInfo.iAcquiredTrohpy);
        //Debug.LogError("iBadgeIndex:" + accountInfo.profileInfo.iBadgeIndex);
        //Debug.LogError("iClearedBossMission:" + accountInfo.profileInfo.iClearedBossMission);
        //Debug.LogError("iClearedGeneralMission:" + accountInfo.profileInfo.iClearedGeneralMission);
        //Debug.LogError("iClearedLastStage:" + accountInfo.profileInfo.iClearedLastStage);
        //Debug.LogError("iDefeatedBossMonsterCount:" + accountInfo.profileInfo.iDefeatedBossMonsterCount);
        //Debug.LogError("iDefeatedMonsterCount:" + accountInfo.profileInfo.iDefeatedMonsterCount);
        //Debug.LogError("iMaximumDefeatedGoldMonsterLevel:" + accountInfo.profileInfo.iMaximumDefeatedGoldMonsterLevel);
        //Debug.LogError("iTotalPlayTime:" + accountInfo.profileInfo.iTotalPlayTime);
        //Debug.LogError("-------------------------------------------------");

        LobbySceneInit.instance.clearChallenge = LobbySceneInit.instance.CheckClearCount(NMUserInfo.Ins.currentCollectionInfo);

        if (SteamManager.Initialized)
        {
            for (int i = 0; i < 20; i++)
            {
                if (i <= LobbySceneInit.instance.clearChallenge)
                {
                    LobbySceneInit.instance.AchievementStage(i);
                }
            }
        }
        if (MenuTowerInfo.instance != null)
        {
            MenuTowerInfo.instance.AllToggleSetting();
        }
        return true;
    }

    Action<int> OnStartMatchAck;
    private bool StartMatchAck(int Error)
    {
        if (Error < 0)
        {
            Debug.LogError("StartMatchAck:" + Error);
            return true;
        }

        OnStartMatchAck?.Invoke(0);
        return true;
    }

    Action<string> OnStartMatchNot;
    private bool StartMatchNot(long _RoomIndex, string _HostName, int _Port)
    {
        NGNetMatchServer.Ins.RoomIndex = _RoomIndex;
        NGNetMatchServer.Ins.HostName = _HostName;
        NGNetMatchServer.Ins.Port = _Port;
        NGNetMatchServer.Ins.Connect();
        OnStartMatchNot?.Invoke("Ro : " + _RoomIndex + " HostName : " + _HostName + " Port : " + _Port + " JoinRoomReq");
        return true;
    }

    public Action<string> OnChangeNicknameAck;
    private bool ChangeNicknameAck(int Error, string _nick)
    {
        if (Error < 0)
        {
            if (FirstPlayPanelManager.instance != null)
            {
                string _str = "";
                switch (Error)
                {
                    case -115:

                        if (language == Language.English)
                        {
                            _str = "The nickname is taken.";
                        }
                        else if (language == Language.Korean)
                        {
                            _str = "이미 존재하는 닉네임입니다.";
                        }
                        break;
                    default:
                        _str = "Error";
                        break;
                }

                FirstPlayPanelManager.instance.SetErrorMessage(_str);
            }

            Debug.LogError("ChangeNicknameAck:" + Error);
            return true;
        }

        NMUserInfo.Ins.loginInfo.userInfo.NickName = _nick;
        OnChangeNicknameAck?.Invoke(_nick);
        return true;
    }

    public Action<int> OnStartChallengeModeAck;
    private bool StartChallengeModeAck(int Error)
    {
        if (Error < 0)
        {
            Debug.LogError("StartChallengeMode:" + Error);
            return true;
        }

        OnStartChallengeModeAck?.Invoke(0);
        return true;
    }

    public Action<int, NGChallengeModeEndInfo, List<NGCollectionInfo>> OnEndChallengeModeAck;
    private bool EndChallengeModeAck(int Error, NGChallengeModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo)
    {
        if (Error < 0)
        {
            Debug.LogError("EndChallengeMode:" + Error);
            return true;
        }

        OnEndChallengeModeAck?.Invoke(0, _EndInfo, _CollectionInfo);
        return true;
    }

    public Action<int, NGEndlessModeEndInfo, List<NGCollectionInfo>> OnEndEndlessModeAck;
    private bool EndEndlessModeAck(int Error, NGEndlessModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo)
    {
        if (Error < 0)
        {
            Debug.LogError("EndChallengeMode:" + Error);
            return true;
        }

        OnEndEndlessModeAck?.Invoke(0, _EndInfo, _CollectionInfo);
        return true;
    }

    public Action<int, List<NGChallengeRanker>> OnGetChallengeTopRankgerAck;
    private bool GetChallengeTopRankgerAck(int Error, List<NGChallengeRanker> _vecRanker)
    {
        if (Error < 0)
        {
            Debug.LogError("GetChallengeTopRankger:" + Error);
            return true;
        }

        OnGetChallengeTopRankgerAck?.Invoke(0, _vecRanker);
        return true;
    }

    public Action<int, List<NGEndlessModeRanker>> OnGetEndlessTopRankgerAck;
    private bool GetEndlessTopRankgerAck(int Error, List<NGEndlessModeRanker> _vecRanker)
    {
        if (Error < 0)
        {
            Debug.LogError("GetEndlessTopRankgerAck:" + Error);
            return true;
        }

        OnGetEndlessTopRankgerAck?.Invoke(0, _vecRanker);
        return true;
    }

    public Action<int, NGSkillInfo> OnLearnSkillAck;
    private bool LearnSkillAck(int Error, NGSkillInfo _skillInfo)
    {
        if (Error < 0)
        {
            Debug.LogError("LearnSkillAck:" + Error);
            if (Error == -116)
            {
                //이미 배운 스킬입니다.
            }
            return true;
        }
         
        OnLearnSkillAck?.Invoke(0, _skillInfo);
        return true;
    }

    public Action<int, NGSkillInfo> OnDelSkillAck;
    private bool DelSkillAck(int Error, NGSkillInfo _skillInfo)
    {
        if (Error < 0)
        {
            Debug.LogError("OnDelSkillAck:" + Error);
            if (Error == -117)
            {
                //배우지 않은 스킬입니다.
            }
            return true;
        }

        OnDelSkillAck?.Invoke(0, _skillInfo);
        return true;
    }

    #region 챌린지 점수 저장용 함수
    //타이틀 처음에만 보여주고 스킵하도록 하는 변수 JBM_0320
    public bool isSkipTitle = false;
    public bool isChallenge = false;
    public int challengeNum = -1;
    public int usedTechPoint = -1;

    public NGChallengeModeEndInfo endInfo = null;
    public NGEndlessModeEndInfo endInfo_Endless = null;
    [Header("프로필 저장용")]
    public NGProfileInfo addProfileInfo = null;
    public bool isClear = false;
    

    public void ScoreSetting(ChallengeEndPopup endPopUp)
    {
        addProfileInfo = new NGProfileInfo();
        addProfileInfo.iAcquiredEpicTower = endPopUp.epic.iScore;
        addProfileInfo.iClearedGeneralMission = endPopUp.mission.iScore;
        addProfileInfo.iTotalPlayTime = endPopUp.time.iScore;
        addProfileInfo.iMaximumDefeatedGoldMonsterLevel = endPopUp.goldMob.iScore;
        addProfileInfo.iAcquiredGold = endPopUp.gold.iScore;
        addProfileInfo.iDefeatedMonsterCount = endPopUp.kill.iScore;
        addProfileInfo.iDefeatedBossMonsterCount = endPopUp.bossKill.iScore;

        if (isChallenge)
        {
            endInfo = new NGChallengeModeEndInfo();

            endInfo.MapID = challengeNum;
            endInfo.ClearLife = endPopUp.life.iScore;
            endInfo.PlayTime = endPopUp.time.iScore;
            endInfo.EpicTowerCount = endPopUp.epic.iScore;
            endInfo.MissionClearCount = endPopUp.mission.iScore;
            endInfo.GoldMonsterLevel = endPopUp.goldMob.iScore;
            endInfo.KillMonsterCount = endPopUp.kill.iScore;
            endInfo.KillBossMonsterCount = endPopUp.bossKill.iScore;
            endInfo.Gold = endPopUp.gold.iScore;
            endInfo.TotalScore = CalculateScore(endInfo);

            addProfileInfo.iClearedBossMission = MissionManager.instance.GetClearedBossMissionCount();
        }
        if (isEndlessMode)
        {
            endInfo_Endless = new NGEndlessModeEndInfo();

            endInfo_Endless.iMaxRound = endPopUp.maxRound.iScore;
            endInfo_Endless.i64PlayTime = endPopUp.time.iScore;
            endInfo_Endless.iEpicTowerCount = endPopUp.epic.iScore;
            endInfo_Endless.iMissionClearCount = endPopUp.mission.iScore;
            endInfo_Endless.iGoldMonsterLevel = endPopUp.goldMob.iScore;
            endInfo_Endless.iKillMonsterCount = endPopUp.kill.iScore;
            endInfo_Endless.iKillBossMonsterCount = endPopUp.bossKill.iScore;  
            endInfo_Endless.iGold = endPopUp.gold.iScore;
            endInfo_Endless.iUsedTechPoint = usedTechPoint;
            endInfo_Endless.iTotalScore = CalculateScore(endInfo_Endless);

            addProfileInfo.iClearedBossMission = 0;
        }

    }
    private int CalculateScore(NGChallengeModeEndInfo _endInfo) //챌린지용
    {
        int resultInt = 0;

        resultInt += _endInfo.ClearLife * 100;
        resultInt -= (int)_endInfo.PlayTime * 1;
        resultInt += _endInfo.EpicTowerCount * 100;
        resultInt += _endInfo.Gold * 1;
        resultInt += _endInfo.MissionClearCount * 100;
        resultInt += _endInfo.GoldMonsterLevel * 100;
        resultInt += _endInfo.KillMonsterCount * 10;
        resultInt += _endInfo.KillBossMonsterCount * 20;

        if (LevelManager.instance != null)
        {
            if (LevelManager.instance.InfiniteStopWaveTimer)
            {
                resultInt = (int)(resultInt / 2.0f);
            }
        }

        return resultInt;
    }
    private int CalculateScore(NGEndlessModeEndInfo _endInfo_Endless) //무한모드용
    {
        int resultInt = 0;

        resultInt += _endInfo_Endless.iMaxRound * 100;
        resultInt -= (int)_endInfo_Endless.i64PlayTime * 1;
        resultInt += _endInfo_Endless.iEpicTowerCount * 100;
        resultInt += _endInfo_Endless.iGold * 1;
        resultInt += _endInfo_Endless.iMissionClearCount * 100;
        resultInt += _endInfo_Endless.iGoldMonsterLevel * 100;
        resultInt += _endInfo_Endless.iKillMonsterCount * 10;
        resultInt += _endInfo_Endless.iKillBossMonsterCount * 20;

        if (LevelManager.instance != null)
        {
            if (LevelManager.instance.InfiniteStopWaveTimer)
            {
                resultInt = (int)(resultInt / 2.0f);
            }
        }

        return resultInt;
    }
    #endregion 챌린지 점수 저장용 임시함수

    [Header("최초접속 확인용")]
    public bool isFirstPlay = false;

    public enum Language
    {
        Korean,
        English
    }
    [Header("언어 저장")]
    private Language _language;
    public Language language
    {
        get
        {
            return _language;
        }
        set
        {
            _language = value;

            if (value == Language.Korean)
            {
                Lean.Localization.LeanLocalization.CurrentLanguage = "Korean";
                _language = value;
            }
            else if (value == Language.English)
            {
                Lean.Localization.LeanLocalization.CurrentLanguage = "English";
                _language = value;
            }
            else
            {
                Lean.Localization.LeanLocalization.CurrentLanguage = "English";
                _language = Language.English;
            }
        }
    }

    #region 이지모드
    [Header("이지모드 확인용")]
    public bool isNormalMode = false;
    #endregion 이지모드

    #region 무한모드
    [Header("무한모드 확인")]
    public bool isEndlessMode = false;
    #endregion 무한모드
}
