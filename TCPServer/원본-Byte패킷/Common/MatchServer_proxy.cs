using System;
using System.Net;
using System.Collections.Generic;


using NGNet;
namespace MatchServerC2S
{
	public class Proxy : NGNet.RmiProxy
	{
		public void JoinRoomReq( RmiContext rmiContext, Int64 _RoomIndex, Int64 _AccountID, string _Nickname )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _Nickname );
			RmiSend( rmiContext, Common.Rmi_JoinRoomReq, msg );
		}
		public void ReadyRoomReq( RmiContext rmiContext, Int64 _RoomIndex )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			RmiSend( rmiContext, Common.Rmi_ReadyRoomReq, msg );
		}
		public void BaseSyncNot( RmiContext rmiContext, Int64 _RoomIndex, NGBaseFrameSync _BaseSync )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _BaseSync );
			RmiSend( rmiContext, Common.Rmi_BaseSyncNot, msg );
		}
		public void FrameSyncNot( RmiContext rmiContext, Int64 _RoomIndex, List<NGFrameSync> _vecSync )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _vecSync );
			RmiSend( rmiContext, Common.Rmi_FrameSyncNot, msg );
		}
		public void StartGameReq( RmiContext rmiContext, Int64 _RoomIndex )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			RmiSend( rmiContext, Common.Rmi_StartGameReq, msg );
		}
		public void EndGameReq( RmiContext rmiContext, Int64 _RoomIndex, Int64 _AccountID, List<NGPlayEndInfo> _Info )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _Info );
			RmiSend( rmiContext, Common.Rmi_EndGameReq, msg );
		}
		public void OnWaveStateNot( RmiContext rmiContext, Int64 _RoomIndex, int _PlayerState, int _MissionRand, List<int> _vecAttack )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _PlayerState );
			UMessageMarshal.Write( msg, _MissionRand );
			UMessageMarshal.Write( msg, _vecAttack );
			RmiSend( rmiContext, Common.Rmi_OnWaveStateNot, msg );
		}
		public void RequestFrameSyncReq( RmiContext rmiContext, Int64 _RoomIndex, Int64 _TargetAccountID )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _TargetAccountID );
			RmiSend( rmiContext, Common.Rmi_RequestFrameSyncReq, msg );
		}
		public void SystemNoticeNot( RmiContext rmiContext, Int64 _RoomIndex, Int64 _AccountID, string _Chat )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _Chat );
			RmiSend( rmiContext, Common.Rmi_SystemNoticeNot, msg );
		}
		public void LeaveRoomReq( RmiContext rmiContext, Int64 _RoomIndex )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			RmiSend( rmiContext, Common.Rmi_LeaveRoomReq, msg );
		}
		public void OngoingGameResultNot( RmiContext rmiContext, Int64 _RoomIndex, List<NGPlayEndInfo> _vecInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _vecInfo );
			RmiSend( rmiContext, Common.Rmi_OngoingGameResultNot, msg );
		}
		public void SystemNoticeNot2( RmiContext rmiContext, Int64 _RoomIndex, int Type, int value )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, Type );
			UMessageMarshal.Write( msg, value );
			RmiSend( rmiContext, Common.Rmi_SystemNoticeNot2, msg );
		}
		public void RoomUserKickReq( RmiContext rmiContext, Int64 _RoomIndex, Int64 _KickAccountID )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _KickAccountID );
			RmiSend( rmiContext, Common.Rmi_RoomUserKickReq, msg );
		}
		public void VRFrameSyncNot( RmiContext rmiContext, Int64 _RoomIndex, List<NGVRFrameSync> _vecSync )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _vecSync );
			RmiSend( rmiContext, Common.Rmi_VRFrameSyncNot, msg );
		}
	}
}

namespace MatchServerS2C
{
	public class Proxy : NGNet.RmiProxy
	{
		public void JoinRoomAck( RmiContext rmiContext, int Error, NGRoomInfo _Room )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _Room );
			RmiSend( rmiContext, Common.Rmi_JoinRoomAck, msg );
		}
		public void ReadyRoomAck( RmiContext rmiContext, int Error, int _PlayerState )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _PlayerState );
			RmiSend( rmiContext, Common.Rmi_ReadyRoomAck, msg );
		}
		public void ChangeRoomStateNot( RmiContext rmiContext, NGRoomInfo _RoomInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomInfo );
			RmiSend( rmiContext, Common.Rmi_ChangeRoomStateNot, msg );
		}
		public void BaseSyncNot( RmiContext rmiContext, Int64 _AccountID, NGBaseFrameSync _BaseSync )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _BaseSync );
			RmiSend( rmiContext, Common.Rmi_BaseSyncNot, msg );
		}
		public void FrameSyncNot( RmiContext rmiContext, Int64 _AccountID, List<NGFrameSync> _vecSync )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _vecSync );
			RmiSend( rmiContext, Common.Rmi_FrameSyncNot, msg );
		}
		public void StartGameAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_StartGameAck, msg );
		}
		public void StartGameNot( RmiContext rmiContext )
		{
			Message msg = new Message();
			RmiSend( rmiContext, Common.Rmi_StartGameNot, msg );
		}
		public void EndGameAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_EndGameAck, msg );
		}
		public void EndGameNot( RmiContext rmiContext, List<NGPlayEndInfo> _Info )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _Info );
			RmiSend( rmiContext, Common.Rmi_EndGameNot, msg );
		}
		public void OnWaveStateNot( RmiContext rmiContext, int _PlayerState, int _MissionRand, List<int> _vecAttack )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _PlayerState );
			UMessageMarshal.Write( msg, _MissionRand );
			UMessageMarshal.Write( msg, _vecAttack );
			RmiSend( rmiContext, Common.Rmi_OnWaveStateNot, msg );
		}
		public void RequestFrameSyncAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_RequestFrameSyncAck, msg );
		}
		public void SystemNoticeNot( RmiContext rmiContext, Int64 _AccountID, string _Chat )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _Chat );
			RmiSend( rmiContext, Common.Rmi_SystemNoticeNot, msg );
		}
		public void LeaveRoomAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_LeaveRoomAck, msg );
		}
		public void OngoingGameResultNot( RmiContext rmiContext, List<NGPlayEndInfo> _vecInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _vecInfo );
			RmiSend( rmiContext, Common.Rmi_OngoingGameResultNot, msg );
		}
		public void SystemNoticeNot2( RmiContext rmiContext, Int64 _AccountID, int Type, int value )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, Type );
			UMessageMarshal.Write( msg, value );
			RmiSend( rmiContext, Common.Rmi_SystemNoticeNot2, msg );
		}
		public void RoomUserKickAck( RmiContext rmiContext, int Error, NGRoomInfo _Room )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _Room );
			RmiSend( rmiContext, Common.Rmi_RoomUserKickAck, msg );
		}
		public void RoomUserKickNot( RmiContext rmiContext )
		{
			Message msg = new Message();
			RmiSend( rmiContext, Common.Rmi_RoomUserKickNot, msg );
		}
		public void VRFrameSyncNot( RmiContext rmiContext, Int64 _AccountID, List<NGVRFrameSync> _vecSync )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _vecSync );
			RmiSend( rmiContext, Common.Rmi_VRFrameSyncNot, msg );
		}
	}
}

