using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace NGNet
{
    class BasicType
    {
        public const Int32 MAX_PACKET_SIZE = (1024 * 256);
        /*
	     * PacketSize, PacketID, SequenceNumber, RmiContext flag
	     */
        public const Int32 HEADSIZE = sizeof( Int32 ) + sizeof( Int32 ) + sizeof( Int32 ) + sizeof( Byte );
        public const Int32 HEAD_POS_PACKET_SIZE = 0;
        public const Int32 HEAD_POS_PACKET_ID = sizeof( Int32 );
        public const Int32 HEAD_POS_SEQUENCE_NUM = sizeof( Int32 ) + sizeof( Int32 );
        public const Int32 HEAD_POS_RMICONTEXT = sizeof( Int32 ) + sizeof( Int32 ) + sizeof( Int32 );

        public const Int32 PACKETID_HEART_BIT = -1;
        /*
         * server -> client 접속시 알려줌
         */
        public const Int32 PACKETID_SC_HOSTID_INFO = -2;
        /*
	    * S PACKETID_SC_HOSTID_INFO -> clinet
	    * C PACKETID_CS_HOSTID_RECV -> server
	    * client -> server 첫 접속시 받았다고 회신
	    * client 의 HostID 초기값 이면 이 패킷을 보냄
	    */
        public const Int32 PACKETID_CS_HOSTID_RECV = -3;
        /*
        * S PACKETID_SC_HOSTID_INFO -> clinet
        * C PACKETID_CS_HOSTID_RECONNECT -> server	
        * client 의 HostID 이미 존재하면 이 패킷을 보냄
        */
        public const Int32 PACKETID_CS_HOSTID_RECONNECT = -4;	// client -> server 재

        public const Int32 PACKETID_SC_RECONNECT_SUCCESS = -5;
        public const Int32 PACKETID_SC_RECONNECT_FAIL = -6;
    }

    class NGLog
    {
        public static object csLog = new object();
        public static Queue<string> queLog = new Queue<string>();

        [Conditional("UnityEditor")]
        public static void Log( string msg )
        {
#if DEBUG
            //Console.WriteLine( msg );
            //DebugX.Log( msg );
            lock ( queLog )
            {
                if (queLog.Count > 20)
                    queLog.Clear();

                string msgRet = DateTime.Now.ToString();
                msgRet += " : ";
                msgRet += msg;
                queLog.Enqueue( msgRet );
            }
#endif
        }

        [Conditional("UnityEditor")]
        public static void Print()
        {
            lock( csLog )
            {
                int count = queLog.Count;

                if( count == 0 )
                    return;

                for( int i = 0; i < count; ++i )
                {
                    UnityEngine.Debug.Log( queLog.Dequeue() );
                }
            }
        }
    }
}
