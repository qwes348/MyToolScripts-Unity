using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NGNet;
using System.Linq;

namespace Oni.Server
{
    // 리퀘스트를 보내는곳
    public class GameServer_proxy : RmiProxy
    {
        #region 심플 테스트
        public void TestReq(int error)
        {
            //Message msg = new Message();
            //MessageMarshal.Write(msg, error);
            //RmiSend(rmiContext, GameServer_RMI.Rmi_TestAck, msg);

            JsonMessage msg = new JsonMessage();
            msg.Write("error", error);
            RmiSend(GameServer_RMI.Rmi_TestAck, msg, JsonMessage.TargetEnum.AllPlayers);
        }

        public void Test2Req(int error, int testNum, string testString)
        {
            JsonMessage msg = new JsonMessage();
            msg.Write("error", error);
            msg.Write("testNum", testNum);
            msg.Write("testString", testString);
            RmiSend(GameServer_RMI.Rmi_Test2Ack, msg, JsonMessage.TargetEnum.AllPlayers);
        }
        #endregion        
    }
}
