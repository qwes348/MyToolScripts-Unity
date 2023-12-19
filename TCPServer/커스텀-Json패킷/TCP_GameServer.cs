using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NGNet;
using System;
using Oni;
using Sirenix.OdinInspector;

namespace Oni.Server
{
    public class TCP_GameServer : SingletonMono<TCP_GameServer>
    {
        public TCPClient Client { get; private set; }

        private GameServer_proxy proxy = new GameServer_proxy();
        private GameServer_stub stub = new GameServer_stub();

        //public int port;

        public bool IsConnectServer { get => Client != null && Client.IsConnected; }

        public Action<TCPClient> ActionOnConnected;

        public DateTime checkServerInfoTime;
        public DateTime checkReconnectionServerTime;

        #region 프로퍼티
        public GameServer_proxy Proxy { get => proxy; }
        public GameServer_stub Stub { get => stub; }

        #endregion

        private void Start()
        {
            if (Instance == this)
                DontDestroyOnLoad(gameObject);
            else
            {
                Destroy(gameObject);
                return;
            }
            Connect();
        }

        [Button]
        public void Test()
        {
            onTestAck = (error) => Debug.LogFormat("TestAck: {0}", error);
            Connect();
            proxy.TestReq(0);
        }

        [Button]
        public void Test2()
        {
            Connect();
            proxy.Test2Req(1, 125, "125");
        }

        private void Update()
        {
            if (Client != null)
            {
                try
                {
                    Client.FrameMove();
                }
                catch (Exception e)
                {
                    Debug.LogError("GameServer: " + e.Message);
                }
            }
        }

        private void OnDestroy()
        {
            if (Client != null)
                Client.Disconnect();
        }

        // 원본에서는 유저가 서버와 연결돼있는지 확인함 
        public void Connect()
        {
            if (Client == null)
            {
                Client = new TCPClient();
            }

            if (!Client.IsConnected)
            {
                Client.InitClient();

                Client.AttachProxy(proxy);
                Client.AttachStub(stub);

                // Stub에서 메세지를 받았을때 ID에따라 호출될 함수들을 액션에 연결
                // 호출될 함수는 꼭 여기를 통해서 연결하지 않아도 됨
                stub.TestAck = TestAck;
                stub.Test2Ack = Test2Ack;
            }
        }

        /*
         * 타 유저 혹인 서버에게 Rmi를 받았을때
         * 최종적으로 도달하는곳
         * 꼭 여기뿐만아니라 다른곳에 액션을 정의해도 됨
         */
        #region 메세지 받았을때 액션

        public Action<int> onTestAck;
        private bool TestAck(int Error)
        {
            if (Error < 0)
            {
                Debug.LogError("TestAck: " + Error);
                // 에러가 난다해도 이 함수안에 들어왔으면 이 함수가 Stub에 연결됐다는 것이기때문에
                // 무조건 true를 반환해야함
                return true;
            }

            onTestAck?.Invoke(Error);
            return true;
        }

        private void Test2Ack(int error, int testInt, string testString)
        {
            if (error < 0)
            {
                Debug.LogError("Test2Ack: " + error);
                Debug.LogFormat("Test2Ack: {0}, {1}, {2}", error, testInt, testString);
            }
        }

        #endregion
    }
}
