using NGNet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oni.Server
{
    // 메세지를 받고 적절한 액션을 찾아 호출해주는곳
    public class GameServer_stub : NGNet.RmiStub
    {
        // 파라미터가 없는경우
        public delegate bool TestDelegate(int Error);
        public TestDelegate TestAck = delegate (int Error)
        {
            // 아무 액션이 등록돼지 않았다면 false를 리턴함
            return false;
        };

        #region 델리게이트
        #region 심플 테스트
        // 파라미터가 있는경우
        public delegate void Test2Delegate(int error, int testNum, string testString);
        public Test2Delegate Test2Ack;


        #endregion

        #endregion

        // 메세지의 패킷ID를보고 적절한 RMI함수를 호출함 => 이거는 안씀
        public override bool ProcessReceivedMessage(Message msg)
        {
            return false;
        }


        /// <summary>
        /// 메세지의 패킷ID를보고 적절한 RMI함수를 호출함
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool ProcessReceivedMessage(JsonMessage msg)
        {
            switch (msg.ID)
            {
                #region 테스트용
                case GameServer_RMI.Rmi_TestAck:
                    ProcessReceivedMessage_Test(msg);
                    break;
                case GameServer_RMI.Rmi_Test2Ack:
                    ProcessReceivedMessage_Test2(msg);
                    break;
                #endregion
                default:
                    return false;
            }
            return true;
        }


        // ProcessReceivedMessage--------------------------------------------------------------------------------------------

        private bool ResultErrorCheck(JsonMessage msg)
        {
            msg.Read("error", out int error);
            bool isError = error != 0;
            if(isError)
            {
                Debug.LogErrorFormat("MessageError: {0}", error.ToString());
            }

            return !isError;
        }

        #region 심플 테스트
        private void ProcessReceivedMessage_Test(JsonMessage msg)
        {
            //MessageMarshal.Read(msg, out int Error);
            msg.Read("error", out int error);

            bool _ret = TestAck(error);
            // 등록된 액션이 없는경우
            if(_ret == false)
                Debug.LogError("Error: RMI function that a user did not create has been called.");
        }

        private void ProcessReceivedMessage_Test2(JsonMessage msg)
        {
            msg.Read("error", out int error);
            msg.Read("testNum", out int testNum);
            msg.Read("testString", out string testString);

            Test2Ack(error, testNum, testString);
        }
        #endregion
    }
}
