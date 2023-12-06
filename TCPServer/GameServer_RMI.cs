using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oni.Server
{
    /*
     * 서버에 보낼 리퀘스트의 종류를 구분할 ID를 보관하는 클래스
     * 
     */

    public static class GameServer_RMI
    {
        public const int Rmi_TestAck = 1;
        public const int Rmi_Test2Ack = 2;
    }
}