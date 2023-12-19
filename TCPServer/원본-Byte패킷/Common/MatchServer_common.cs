using System;
using System.Collections.Generic;


using NGNet;
namespace MatchServerC2S
{
	public class Common
	{
		// Message ID that replies to each RMI method.
		public const Int32 Rmi_JoinRoomReq = 50000;
		public const Int32 Rmi_ReadyRoomReq = 50001;
		public const Int32 Rmi_BaseSyncNot = 50002;
		public const Int32 Rmi_FrameSyncNot = 50003;
		public const Int32 Rmi_StartGameReq = 50004;
		public const Int32 Rmi_EndGameReq = 50005;
		public const Int32 Rmi_OnWaveStateNot = 50006;
		public const Int32 Rmi_RequestFrameSyncReq = 50007;
		public const Int32 Rmi_SystemNoticeNot = 50008;
		public const Int32 Rmi_LeaveRoomReq = 50009;
		public const Int32 Rmi_OngoingGameResultNot = 50010;
		public const Int32 Rmi_SystemNoticeNot2 = 50011;
		public const Int32 Rmi_RoomUserKickReq = 50012;
		public const Int32 Rmi_VRFrameSyncNot = 50013;
	}
}

namespace MatchServerS2C
{
	public class Common
	{
		// Message ID that replies to each RMI method.
		public const Int32 Rmi_JoinRoomAck = 60000;
		public const Int32 Rmi_ReadyRoomAck = 60001;
		public const Int32 Rmi_ChangeRoomStateNot = 60002;
		public const Int32 Rmi_BaseSyncNot = 60003;
		public const Int32 Rmi_FrameSyncNot = 60004;
		public const Int32 Rmi_StartGameAck = 60005;
		public const Int32 Rmi_StartGameNot = 60006;
		public const Int32 Rmi_EndGameAck = 60007;
		public const Int32 Rmi_EndGameNot = 60008;
		public const Int32 Rmi_OnWaveStateNot = 60009;
		public const Int32 Rmi_RequestFrameSyncAck = 60010;
		public const Int32 Rmi_SystemNoticeNot = 60011;
		public const Int32 Rmi_LeaveRoomAck = 60012;
		public const Int32 Rmi_OngoingGameResultNot = 60013;
		public const Int32 Rmi_SystemNoticeNot2 = 60014;
		public const Int32 Rmi_RoomUserKickAck = 60015;
		public const Int32 Rmi_RoomUserKickNot = 60016;
		public const Int32 Rmi_VRFrameSyncNot = 60017;
	}
}

