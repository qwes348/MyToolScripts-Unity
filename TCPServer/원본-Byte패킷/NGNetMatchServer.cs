using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

using NGNet;
using NGEL;
using MatchServerS2C;
using UnityEngine.SceneManagement;
using TowerDefense.Level;

public class NGNetMatchServer : Singleton2<NGNetMatchServer>
{
    public int Port;
    public string HostName;

    private CNetClient client;
    public MatchServerC2S.Proxy proxy = new MatchServerC2S.Proxy();
    private MatchServerS2C.Stub stub = new MatchServerS2C.Stub();

    public bool isConnectServer = false;

    public Action<int> OnConnectAction;

    public long RoomIndex { get; internal set; }
    public NGRoomInfo roomInfo;

    //RTDVRPrototype_Merge_JBM
    public static string MENUSCENESTRING = "MatchingScene";
    public static string MENUSCENESTRING2 = "MatchingScene2";
    public static string GAME_SCENE_STRING = "RTDVRPrototype_Merge_2";
    public static string GAME_RESULT_SCENE_STRING = "GameResultScene";
    public static string TUTORIAL1_SCENE_STRING = "RTDVRPrototype_Tutorial";
    public static string TUTORIAL2_SCENE_STRING = "RTDVRPrototype_Tutorial2";
    public static string CHALLENGE1_SCENE_STRING = "RTDVRPrototype_Stage1";
    public static string CHALLENGE2_SCENE_STRING = "RTDVRPrototype_Stage2";
    public static string CHALLENGE3_SCENE_STRING = "RTDVRPrototype_Stage3";
    public static string CHALLENGE4_SCENE_STRING = "RTDVRPrototype_Stage4";
    public static string CHALLENGE5_SCENE_STRING = "RTDVRPrototype_Stage5";
    public static string CHALLENGE6_SCENE_STRING = "RTDVRPrototype_Stage6";
    public static string CHALLENGE7_SCENE_STRING = "RTDVRPrototype_Stage7";
    public static string CHALLENGE8_SCENE_STRING = "RTDVRPrototype_Stage8";
    public static string CHALLENGE9_SCENE_STRING = "RTDVRPrototype_Stage9";
    public static string CHALLENGE10_SCENE_STRING = "RTDVRPrototype_Stage10";
    public static string UNION1_SCENE_STRING = "RTDVRPrototype_Union1";
    public static string UNION2_SCENE_STRING = "RTDVRPrototype_Union2";
    public static string UNION3_SCENE_STRING = "RTDVRPrototype_Union3";
    public static string UNION4_SCENE_STRING = "RTDVRPrototype_Union4";
    public static string UNION5_SCENE_STRING = "RTDVRPrototype_Union5";
    public static string UNION6_SCENE_STRING = "RTDVRPrototype_Union6";
    public static string UNION7_SCENE_STRING = "RTDVRPrototype_Union7";
    public static string UNION8_SCENE_STRING = "RTDVRPrototype_Union8";
    public static string UNION9_SCENE_STRING = "RTDVRPrototype_Union9";
    public static string UNION10_SCENE_STRING = "RTDVRPrototype_Union10";
    public static string ENDLESS_SCENE = "RTDVRPrototype_Endless";

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
            param.hostname = HostName;
            param.port = Port;
            param.OnJoinServerComplete = OnJoinServerComplete;
            param.OnLeaveServer = OnLeaveServer;

            stub.JoinRoomAck = JoinRoomAck;
            stub.StartGameAck = StartGameAck;
            stub.StartGameNot = StartGameNot;
            stub.ReadyRoomAck = ReadyRoomAck;
            stub.BaseSyncNot = BaseSyncNot;
            stub.FrameSyncNot = FrameSyncNot;
            stub.SystemNoticeNot = SystemNoticeNot;
            stub.EndGameAck = EndGameAck;
            stub.EndGameNot = EndGameNot;
            stub.ChangeRoomStateNot = ChangeRoomStateNot;
            //VR추가 JBM_1108
            stub.VRFrameSyncNot = VRFrameSyncNot;
            stub.SystemNoticeNot2 = SystemNoticeNot2;

            stub.RoomUserKickAck = RoomUserKickAck;
            stub.RoomUserKickNot = RoomUserKickNot;

            stub.OnWaveStateNot = OnWaveStateNot;
            stub.RequestFrameSyncAck = RequestFrameSyncAck;

            client.AttachProxy(proxy);
            client.AttachStub(stub);
            client.Connect(param);

            Debug.Log("matchServerConnect : " + DateTime.Now.ToString());
        }
    }

    private void Update()
    {
        if (client != null)
        {
            client.FrameMove();
            //try
            //{
            //    client.FrameMove();
            //}
            //catch (Exception e)
            //{
            //    Debug.Log("NGNetGameServer" + e.Message);
            //}
        }
    }

    public bool IsRealConnectServer()
    {
        if (client == null)
            return false;

        return client.IsConnected();
    }

    private void OnDestroy()
    {
        if (client != null)
        {
            proxy.LeaveRoomReq(NGNet.RmiContext.Reliable, RoomIndex);
            client.Disconnect();
            client = null;
            isConnectServer = false;
        }
    }

    public void Disconnect()
    {
        if (client != null)
        {
            proxy.LeaveRoomReq(NGNet.RmiContext.Reliable, NGNetMatchServer.Ins.RoomIndex);
            client.Disconnect();
            client = null;
            isConnectServer = false;
        }
        roomInfo = null;
        RoomIndex = -1;
    }

    public void OnJoinServerComplete(ErrorType errorType)
    {
        Debug.Log("OnJoinServerComplete : " + errorType.ToString() + ", " + DateTime.Now.ToString());

        if (errorType == ErrorType.ErrorType_Ok)
        {
            isConnectServer = true;
        }

        OnConnectAction?.Invoke((int)errorType);
        proxy.JoinRoomReq(RmiContext.Reliable, RoomIndex, NMUserInfo.Ins.GetAccountId(), NMUserInfo.Ins.GetNick());
    }

    public void OnLeaveServer(ErrorType errorType)
    {
        Debug.Log("OnLeaveServer : " + errorType.ToString());
    }

    private bool EndRequestFrameSyncAck(int Error)
    {
        if (Error < 0)
        {
            Debug.LogError("EndRequestFrameSyncAck:" + Error);
            return true;
        }
        return true;
    }

    public Action<long> OnRequestFrameSyncNot;

    private bool RequestFrameSyncNot(long _RequestAccountID)
    {
        OnRequestFrameSyncNot?.Invoke(_RequestAccountID);
        return true;
    }

    public Action<int> OnRequestFrameSyncAck;

    private bool RequestFrameSyncAck(int Error)
    {
        if (Error < 0)
        {
            Debug.LogError("RequestFrameSyncAck:" + Error);
            return true;
        }
        OnRequestFrameSyncAck?.Invoke(Error);
        return true;
    }

    public Action<List<NGPlayEndInfo>> OnEndGameNot;

    private bool EndGameNot(List<NGPlayEndInfo> info)
    {
        OnEndGameNot?.Invoke(info);
        return true;
    }

    private bool EndGameAck(int Error)
    {
        if (Error < 0)
        {
            Debug.LogError("EndGameAck:" + Error);
            return true;
        }
        return true;
    }

    public Action<Int64, List<NGFrameSync>> OnFrameSyncNot;

    private bool FrameSyncNot(Int64 _AccountID, List<NGFrameSync> _Sync)
    {
        OnFrameSyncNot?.Invoke(_AccountID, _Sync);
        return true;
    }

    //VR모드 추가 JBM_1106
    public Action<Int64, List<NGVRFrameSync>> OnVRFrameSyncNot; 

    private bool VRFrameSyncNot(Int64 _AccountID, List<NGVRFrameSync> _Sync)
    {
        OnVRFrameSyncNot?.Invoke(_AccountID, _Sync);
        return true;
    }

    public Action<int> OnJoinRoomNot;
    private bool JoinRoomNot(int _TargetAccountID)
    {
        OnJoinRoomNot?.Invoke(_TargetAccountID);
        return true;
    }

    public Action<int> OnRoomUserKickAck;
    private bool RoomUserKickAck(int Error, NGRoomInfo _Room)
    {
        OnRoomUserKickAck?.Invoke(Error);
        return true;
    }

    private bool RoomUserKickNot()
    {
        proxy.LeaveRoomReq(NGNet.RmiContext.Reliable, NGNetMatchServer.Ins.RoomIndex);
        Disconnect();
        return true;
    }

    public Action<int> OnJoinRoomAck;

    private bool JoinRoomAck(int Error, NGRoomInfo _Room)
    {
        if (Error < 0)
        {
            Debug.LogError("JoinRoomAck:" + Error);
            return true;
        }
        roomInfo = _Room;
        RoomIndex = _Room.roomIndex;
        OnJoinRoomAck?.Invoke(0);
        return true;
    }

    public Action<long, string> OnSystemNoticeNot;

    private bool SystemNoticeNot(long _AccountID, string _Chat)
    {
        OnSystemNoticeNot?.Invoke(_AccountID, _Chat);
        return true;
    }

    public Action<long, int, int> OnSystemNoticeNot2;
    private bool SystemNoticeNot2(Int64 _AccountID, int _type, int _value)
    {
        OnSystemNoticeNot2?.Invoke(_AccountID, _type, _value);
        return true;
    }

    public Action<string> OnChangeMissionNot;

    private bool ChangeMissionNot(Int64 _AccountID, string _Chat)
    {
        OnChangeMissionNot?.Invoke(_Chat);
        return true;
    }

    public Action<int> OnChangeRoomStateNot;

    private bool ChangeRoomStateNot(NGRoomInfo _RoomInfo)
    {
        roomInfo = _RoomInfo;
        RoomIndex = _RoomInfo.roomIndex;
        OnChangeRoomStateNot?.Invoke(0);
        return true;
    }

    public Action<int> OnReadyRoomAck;

    private bool ReadyRoomAck(int Error, int _PlayerState)
    {
        if (Error < 0)
        {
            Debug.LogError("ReadyRoomAck:" + Error);
            return true;
        }
        var playerInfo = roomInfo.vecPlayerInfo.Find(_ => _.AccountID == NMUserInfo.Ins.GetAccountId());
        if (playerInfo != null)
            playerInfo.playerState = _PlayerState;
        OnReadyRoomAck?.Invoke(0);
        return true;
    }

    private bool StartGameNot()
    {
        StartCoroutine(StartTimer());
        return true;
    }

    public IEnumerator StartTimer()
    {
        if (NewMenuManager.instance != null)
        {
            NewMenuManager.instance.menuMatchingPopUp.gameObject.SetActive(true);
            NewMenuManager.instance.menuMatchingPopUp.LoadScene(GAME_SCENE_STRING);
            if (SoundController.instance != null)
            {
                SoundController.instance.PlaySound("misc_45");
            }

            float t = 5;
            while (t > 0)
            {
                NewMenuManager.instance.menuMatchingPopUp.loadingTxt.text = "잠시후 게임에 입장합니다. (" + t + ")";

                yield return new WaitForSeconds(1f);
                t -= 1;
            }

            NewMenuManager.instance.menuMatchingPopUp.isTimeOver = true;
        }
    }

    private bool StartGameAck(int Error)
    {
        if (Error < 0)
        {
            Debug.LogError("StartGameAck:" + Error);
            return true;
        }
        return true;
    }

    public Action<long, NGBaseFrameSync> OnBaseSyncNot;//1010 수정

    private bool BaseSyncNot(long _AccountID, NGBaseFrameSync _BaseSync)
    {
        OnBaseSyncNot?.Invoke(_AccountID, _BaseSync);
        return true;
    }

    public Action<int, int, List<int>> OnOnWaveStateNot;

    private bool OnWaveStateNot(int _PlayerState, int _MissionRand, List<int> _vecAttack)
    {
        OnOnWaveStateNot?.Invoke(_PlayerState, _MissionRand, _vecAttack);
        return true;
    }
}