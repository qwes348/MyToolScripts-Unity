using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

using NGNet;
using NGEL;
using Steamworks;

public class NGNetCenterServer : MonoBehaviour
{
    CNetClient client;
    public CenterServerC2S.Proxy proxy = new CenterServerC2S.Proxy();
    private CenterServerS2C.Stub stub = new CenterServerS2C.Stub();
    bool isConnectServer = false;

    public void Connect(string serverIp, int serverPort)
    {
        if (client == null)
        {
            client = new CNetClient();
        }

        if (client.IsConnected() == false)
        {
            isConnectServer = false;
            CNetConnectionParam param = new CNetConnectionParam();

            param.hostname = serverIp;
            param.port = serverPort;

            param.OnJoinServerComplete = OnJoinServerComplete;
            param.OnLeaveServer = OnLeaveServer;

            client.AttachProxy(proxy);
            client.AttachStub(stub);

            client.Connect(param);

            stub.CreateAccountInfoAck = CreateAccountInfoAck;
            stub.GetConnectGameServerInfoAck = GetConnectGameServerInfoAck;
            stub.GetServerGroupInfoAck = GetServerGroupInfoAck;
            Debug.Log("센터서버 접속 시도 : " + DateTime.Now.ToString());
        }

    }

    private void Update()
    {
        if (client != null)
            client.FrameMove();

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
        if (errorType != ErrorType.ErrorType_Ok)
        {
            Debug.Log("센터서버 접속 실패 : " + errorType);
            isConnectServer = true;
        }

        isConnectServer = true;
        proxy.GetServerGroupInfoReq(RmiContext.Reliable, NMAuth.GetID(), (int)NMAuth.GetAuthType(), (int)NGDeviceType.AOS);

        //if (SteamUser.GetSteamID() != null) //스팀연동실패 수정필요 JBM_0416
        //{
        //    proxy.GetServerGroupInfoReq(RmiContext.Reliable, SteamUser.GetSteamID().ToString(), (int)NMAuth.GetAuthType(), (int)NGDeviceType.AOS);
        //    Debug.LogError("getSteam: " + SteamUser.GetSteamID().ToString());
        //}
        //else
        //{
        //    proxy.GetServerGroupInfoReq(RmiContext.Reliable, NMAuth.GetID(), (int)NMAuth.GetAuthType(), (int)NGDeviceType.AOS);
        //    Debug.LogError("getauth: " + NMAuth.GetID());
        //}
        Debug.Log("센터서버 접속됨: " + errorType.ToString() + DateTime.Now.ToString());
    }

    public void OnLeaveServer(ErrorType errorType)
    {
        Debug.Log("센터서버 접속종료: " + errorType.ToString());
    }

    private bool CreateAccountInfoAck(int Error, NGAccountInfo _info)
    {
        NMUserInfo.Ins.AccountInfo = _info;
        proxy.GetConnectGameServerInfoReq(RmiContext.Reliable, _info.ServerGroup);
        return true;
    }

    public Action<CNetClient> ActionGetConnectGameServerInfoAck;
    public bool GetConnectGameServerInfoAck(int iError, string strHostName, int iPort)
    {
        if (iError != 0)
        {
            Debug.Log("GetConnectGameServerInfoAck:" + iError);
            isConnectServer = true;
        }

        NGNetGameServer.Ins.hostname = strHostName;
        NGNetGameServer.Ins.port = iPort;

        Debug.Log("게임서버 정보 확인: " + iError.ToString());
        ActionGetConnectGameServerInfoAck?.Invoke(client);
        return true;
    }

    private bool GetServerGroupInfoAck( int Error, List<NGServerGroupInfo> _vecServerGroupInfo, List<NGAccountInfo> _vecAccountInfo )
    {
        if (Error == 0)
        {
            if (_vecAccountInfo.Count != 0)
                NMAuth.SetAccountAck(_vecAccountInfo);

            //접속정보가 존재하면 기존 접속했던 서버로 접속
            NGServerGroupInfo test = _vecServerGroupInfo.Count > 0 ? _vecServerGroupInfo[0] : new NGServerGroupInfo();
            foreach (var item in _vecAccountInfo)
            {
                if (item.ServerGroup == test.GroupID)
                {
                    NMUserInfo.Ins.AccountInfo = item;
                    proxy.GetConnectGameServerInfoReq(RmiContext.Reliable, test.GroupID);
                    return true;
                }
            }
            proxy.CreateAccountInfoReq(RmiContext.Reliable, NMAuth.GetID(), (int)NMAuth.GetAuthType(), "KR", 1);
        }

        return true;
    }
}
