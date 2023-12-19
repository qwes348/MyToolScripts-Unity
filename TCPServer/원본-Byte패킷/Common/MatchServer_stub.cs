using System;
using System.Net;
using System.Collections.Generic;


using NGNet;
namespace MatchServerC2S
{
	public class Stub : NGNet.RmiStub
	{
		public delegate bool JoinRoomReqDelegate( Int64 _RoomIndex, Int64 _AccountID, string _Nickname );
		public JoinRoomReqDelegate JoinRoomReq = delegate ( Int64 _RoomIndex, Int64 _AccountID, string _Nickname )
		{
			return false;
		};
		public delegate bool ReadyRoomReqDelegate( Int64 _RoomIndex );
		public ReadyRoomReqDelegate ReadyRoomReq = delegate ( Int64 _RoomIndex )
		{
			return false;
		};
		public delegate bool BaseSyncNotDelegate( Int64 _RoomIndex, NGBaseFrameSync _BaseSync );
		public BaseSyncNotDelegate BaseSyncNot = delegate ( Int64 _RoomIndex, NGBaseFrameSync _BaseSync )
		{
			return false;
		};
		public delegate bool FrameSyncNotDelegate( Int64 _RoomIndex, List<NGFrameSync> _vecSync );
		public FrameSyncNotDelegate FrameSyncNot = delegate ( Int64 _RoomIndex, List<NGFrameSync> _vecSync )
		{
			return false;
		};
		public delegate bool StartGameReqDelegate( Int64 _RoomIndex );
		public StartGameReqDelegate StartGameReq = delegate ( Int64 _RoomIndex )
		{
			return false;
		};
		public delegate bool EndGameReqDelegate( Int64 _RoomIndex, Int64 _AccountID, List<NGPlayEndInfo> _Info );
		public EndGameReqDelegate EndGameReq = delegate ( Int64 _RoomIndex, Int64 _AccountID, List<NGPlayEndInfo> _Info )
		{
			return false;
		};
		public delegate bool OnWaveStateNotDelegate( Int64 _RoomIndex, int _PlayerState, int _MissionRand, List<int> _vecAttack );
		public OnWaveStateNotDelegate OnWaveStateNot = delegate ( Int64 _RoomIndex, int _PlayerState, int _MissionRand, List<int> _vecAttack )
		{
			return false;
		};
		public delegate bool RequestFrameSyncReqDelegate( Int64 _RoomIndex, Int64 _TargetAccountID );
		public RequestFrameSyncReqDelegate RequestFrameSyncReq = delegate ( Int64 _RoomIndex, Int64 _TargetAccountID )
		{
			return false;
		};
		public delegate bool SystemNoticeNotDelegate( Int64 _RoomIndex, Int64 _AccountID, string _Chat );
		public SystemNoticeNotDelegate SystemNoticeNot = delegate ( Int64 _RoomIndex, Int64 _AccountID, string _Chat )
		{
			return false;
		};
		public delegate bool LeaveRoomReqDelegate( Int64 _RoomIndex );
		public LeaveRoomReqDelegate LeaveRoomReq = delegate ( Int64 _RoomIndex )
		{
			return false;
		};
		public delegate bool OngoingGameResultNotDelegate( Int64 _RoomIndex, List<NGPlayEndInfo> _vecInfo );
		public OngoingGameResultNotDelegate OngoingGameResultNot = delegate ( Int64 _RoomIndex, List<NGPlayEndInfo> _vecInfo )
		{
			return false;
		};
		public delegate bool SystemNoticeNot2Delegate( Int64 _RoomIndex, int Type, int value );
		public SystemNoticeNot2Delegate SystemNoticeNot2 = delegate ( Int64 _RoomIndex, int Type, int value )
		{
			return false;
		};
		public delegate bool RoomUserKickReqDelegate( Int64 _RoomIndex, Int64 _KickAccountID );
		public RoomUserKickReqDelegate RoomUserKickReq = delegate ( Int64 _RoomIndex, Int64 _KickAccountID )
		{
			return false;
		};
		public delegate bool VRFrameSyncNotDelegate( Int64 _RoomIndex, List<NGVRFrameSync> _vecSync );
		public VRFrameSyncNotDelegate VRFrameSyncNot = delegate ( Int64 _RoomIndex, List<NGVRFrameSync> _vecSync )
		{
			return false;
		};
		public override bool ProcessReceivedMessage( Message msg )
		{
			switch( msg.ID )
			{
			case Common.Rmi_JoinRoomReq:
				ProcessReceivedMessage_JoinRoomReq( msg );
				break;
			case Common.Rmi_ReadyRoomReq:
				ProcessReceivedMessage_ReadyRoomReq( msg );
				break;
			case Common.Rmi_BaseSyncNot:
				ProcessReceivedMessage_BaseSyncNot( msg );
				break;
			case Common.Rmi_FrameSyncNot:
				ProcessReceivedMessage_FrameSyncNot( msg );
				break;
			case Common.Rmi_StartGameReq:
				ProcessReceivedMessage_StartGameReq( msg );
				break;
			case Common.Rmi_EndGameReq:
				ProcessReceivedMessage_EndGameReq( msg );
				break;
			case Common.Rmi_OnWaveStateNot:
				ProcessReceivedMessage_OnWaveStateNot( msg );
				break;
			case Common.Rmi_RequestFrameSyncReq:
				ProcessReceivedMessage_RequestFrameSyncReq( msg );
				break;
			case Common.Rmi_SystemNoticeNot:
				ProcessReceivedMessage_SystemNoticeNot( msg );
				break;
			case Common.Rmi_LeaveRoomReq:
				ProcessReceivedMessage_LeaveRoomReq( msg );
				break;
			case Common.Rmi_OngoingGameResultNot:
				ProcessReceivedMessage_OngoingGameResultNot( msg );
				break;
			case Common.Rmi_SystemNoticeNot2:
				ProcessReceivedMessage_SystemNoticeNot2( msg );
				break;
			case Common.Rmi_RoomUserKickReq:
				ProcessReceivedMessage_RoomUserKickReq( msg );
				break;
			case Common.Rmi_VRFrameSyncNot:
				ProcessReceivedMessage_VRFrameSyncNot( msg );
				break;
			default: return false;
			}
			return true;
		}
		void ProcessReceivedMessage_JoinRoomReq( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			string _Nickname; UMessageMarshal.Read( msg, out _Nickname );
			// Call this method.
			bool _ret = JoinRoomReq( _RoomIndex, _AccountID, _Nickname );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_ReadyRoomReq( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			// Call this method.
			bool _ret = ReadyRoomReq( _RoomIndex );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_BaseSyncNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			NGBaseFrameSync _BaseSync; UMessageMarshal.Read( msg, out _BaseSync );
			// Call this method.
			bool _ret = BaseSyncNot( _RoomIndex, _BaseSync );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_FrameSyncNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			List<NGFrameSync> _vecSync; UMessageMarshal.Read( msg, out _vecSync );
			// Call this method.
			bool _ret = FrameSyncNot( _RoomIndex, _vecSync );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartGameReq( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			// Call this method.
			bool _ret = StartGameReq( _RoomIndex );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_EndGameReq( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			List<NGPlayEndInfo> _Info; UMessageMarshal.Read( msg, out _Info );
			// Call this method.
			bool _ret = EndGameReq( _RoomIndex, _AccountID, _Info );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_OnWaveStateNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			int _PlayerState; UMessageMarshal.Read( msg, out _PlayerState );
			int _MissionRand; UMessageMarshal.Read( msg, out _MissionRand );
			List<int> _vecAttack; UMessageMarshal.Read( msg, out _vecAttack );
			// Call this method.
			bool _ret = OnWaveStateNot( _RoomIndex, _PlayerState, _MissionRand, _vecAttack );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_RequestFrameSyncReq( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			Int64 _TargetAccountID; UMessageMarshal.Read( msg, out _TargetAccountID );
			// Call this method.
			bool _ret = RequestFrameSyncReq( _RoomIndex, _TargetAccountID );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_SystemNoticeNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			string _Chat; UMessageMarshal.Read( msg, out _Chat );
			// Call this method.
			bool _ret = SystemNoticeNot( _RoomIndex, _AccountID, _Chat );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_LeaveRoomReq( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			// Call this method.
			bool _ret = LeaveRoomReq( _RoomIndex );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_OngoingGameResultNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			List<NGPlayEndInfo> _vecInfo; UMessageMarshal.Read( msg, out _vecInfo );
			// Call this method.
			bool _ret = OngoingGameResultNot( _RoomIndex, _vecInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_SystemNoticeNot2( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			int Type; UMessageMarshal.Read( msg, out Type );
			int value; UMessageMarshal.Read( msg, out value );
			// Call this method.
			bool _ret = SystemNoticeNot2( _RoomIndex, Type, value );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_RoomUserKickReq( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			Int64 _KickAccountID; UMessageMarshal.Read( msg, out _KickAccountID );
			// Call this method.
			bool _ret = RoomUserKickReq( _RoomIndex, _KickAccountID );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_VRFrameSyncNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			List<NGVRFrameSync> _vecSync; UMessageMarshal.Read( msg, out _vecSync );
			// Call this method.
			bool _ret = VRFrameSyncNot( _RoomIndex, _vecSync );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
	}
}

namespace MatchServerS2C
{
	public class Stub : NGNet.RmiStub
	{
		public delegate bool JoinRoomAckDelegate( int Error, NGRoomInfo _Room );
		public JoinRoomAckDelegate JoinRoomAck = delegate ( int Error, NGRoomInfo _Room )
		{
			return false;
		};
		public delegate bool ReadyRoomAckDelegate( int Error, int _PlayerState );
		public ReadyRoomAckDelegate ReadyRoomAck = delegate ( int Error, int _PlayerState )
		{
			return false;
		};
		public delegate bool ChangeRoomStateNotDelegate( NGRoomInfo _RoomInfo );
		public ChangeRoomStateNotDelegate ChangeRoomStateNot = delegate ( NGRoomInfo _RoomInfo )
		{
			return false;
		};
		public delegate bool BaseSyncNotDelegate( Int64 _AccountID, NGBaseFrameSync _BaseSync );
		public BaseSyncNotDelegate BaseSyncNot = delegate ( Int64 _AccountID, NGBaseFrameSync _BaseSync )
		{
			return false;
		};
		public delegate bool FrameSyncNotDelegate( Int64 _AccountID, List<NGFrameSync> _vecSync );
		public FrameSyncNotDelegate FrameSyncNot = delegate ( Int64 _AccountID, List<NGFrameSync> _vecSync )
		{
			return false;
		};
		public delegate bool StartGameAckDelegate( int Error );
		public StartGameAckDelegate StartGameAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool StartGameNotDelegate(  );
		public StartGameNotDelegate StartGameNot = delegate (  )
		{
			return false;
		};
		public delegate bool EndGameAckDelegate( int Error );
		public EndGameAckDelegate EndGameAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool EndGameNotDelegate( List<NGPlayEndInfo> _Info );
		public EndGameNotDelegate EndGameNot = delegate ( List<NGPlayEndInfo> _Info )
		{
			return false;
		};
		public delegate bool OnWaveStateNotDelegate( int _PlayerState, int _MissionRand, List<int> _vecAttack );
		public OnWaveStateNotDelegate OnWaveStateNot = delegate ( int _PlayerState, int _MissionRand, List<int> _vecAttack )
		{
			return false;
		};
		public delegate bool RequestFrameSyncAckDelegate( int Error );
		public RequestFrameSyncAckDelegate RequestFrameSyncAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool SystemNoticeNotDelegate( Int64 _AccountID, string _Chat );
		public SystemNoticeNotDelegate SystemNoticeNot = delegate ( Int64 _AccountID, string _Chat )
		{
			return false;
		};
		public delegate bool LeaveRoomAckDelegate( int Error );
		public LeaveRoomAckDelegate LeaveRoomAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool OngoingGameResultNotDelegate( List<NGPlayEndInfo> _vecInfo );
		public OngoingGameResultNotDelegate OngoingGameResultNot = delegate ( List<NGPlayEndInfo> _vecInfo )
		{
			return false;
		};
		public delegate bool SystemNoticeNot2Delegate( Int64 _AccountID, int Type, int value );
		public SystemNoticeNot2Delegate SystemNoticeNot2 = delegate ( Int64 _AccountID, int Type, int value )
		{
			return false;
		};
		public delegate bool RoomUserKickAckDelegate( int Error, NGRoomInfo _Room );
		public RoomUserKickAckDelegate RoomUserKickAck = delegate ( int Error, NGRoomInfo _Room )
		{
			return false;
		};
		public delegate bool RoomUserKickNotDelegate(  );
		public RoomUserKickNotDelegate RoomUserKickNot = delegate (  )
		{
			return false;
		};
		public delegate bool VRFrameSyncNotDelegate( Int64 _AccountID, List<NGVRFrameSync> _vecSync );
		public VRFrameSyncNotDelegate VRFrameSyncNot = delegate ( Int64 _AccountID, List<NGVRFrameSync> _vecSync )
		{
			return false;
		};
		public override bool ProcessReceivedMessage( Message msg )
		{
			switch( msg.ID )
			{
			case Common.Rmi_JoinRoomAck:
				ProcessReceivedMessage_JoinRoomAck( msg );
				break;
			case Common.Rmi_ReadyRoomAck:
				ProcessReceivedMessage_ReadyRoomAck( msg );
				break;
			case Common.Rmi_ChangeRoomStateNot:
				ProcessReceivedMessage_ChangeRoomStateNot( msg );
				break;
			case Common.Rmi_BaseSyncNot:
				ProcessReceivedMessage_BaseSyncNot( msg );
				break;
			case Common.Rmi_FrameSyncNot:
				ProcessReceivedMessage_FrameSyncNot( msg );
				break;
			case Common.Rmi_StartGameAck:
				ProcessReceivedMessage_StartGameAck( msg );
				break;
			case Common.Rmi_StartGameNot:
				ProcessReceivedMessage_StartGameNot( msg );
				break;
			case Common.Rmi_EndGameAck:
				ProcessReceivedMessage_EndGameAck( msg );
				break;
			case Common.Rmi_EndGameNot:
				ProcessReceivedMessage_EndGameNot( msg );
				break;
			case Common.Rmi_OnWaveStateNot:
				ProcessReceivedMessage_OnWaveStateNot( msg );
				break;
			case Common.Rmi_RequestFrameSyncAck:
				ProcessReceivedMessage_RequestFrameSyncAck( msg );
				break;
			case Common.Rmi_SystemNoticeNot:
				ProcessReceivedMessage_SystemNoticeNot( msg );
				break;
			case Common.Rmi_LeaveRoomAck:
				ProcessReceivedMessage_LeaveRoomAck( msg );
				break;
			case Common.Rmi_OngoingGameResultNot:
				ProcessReceivedMessage_OngoingGameResultNot( msg );
				break;
			case Common.Rmi_SystemNoticeNot2:
				ProcessReceivedMessage_SystemNoticeNot2( msg );
				break;
			case Common.Rmi_RoomUserKickAck:
				ProcessReceivedMessage_RoomUserKickAck( msg );
				break;
			case Common.Rmi_RoomUserKickNot:
				ProcessReceivedMessage_RoomUserKickNot( msg );
				break;
			case Common.Rmi_VRFrameSyncNot:
				ProcessReceivedMessage_VRFrameSyncNot( msg );
				break;
			default: return false;
			}
			return true;
		}
		void ProcessReceivedMessage_JoinRoomAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGRoomInfo _Room; UMessageMarshal.Read( msg, out _Room );
			// Call this method.
			bool _ret = JoinRoomAck( Error, _Room );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_ReadyRoomAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			int _PlayerState; UMessageMarshal.Read( msg, out _PlayerState );
			// Call this method.
			bool _ret = ReadyRoomAck( Error, _PlayerState );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_ChangeRoomStateNot( Message msg )
		{
			NGRoomInfo _RoomInfo; UMessageMarshal.Read( msg, out _RoomInfo );
			// Call this method.
			bool _ret = ChangeRoomStateNot( _RoomInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_BaseSyncNot( Message msg )
		{
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			NGBaseFrameSync _BaseSync; UMessageMarshal.Read( msg, out _BaseSync );
			// Call this method.
			bool _ret = BaseSyncNot( _AccountID, _BaseSync );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_FrameSyncNot( Message msg )
		{
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			List<NGFrameSync> _vecSync; UMessageMarshal.Read( msg, out _vecSync );
			// Call this method.
			bool _ret = FrameSyncNot( _AccountID, _vecSync );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartGameAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = StartGameAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartGameNot( Message msg )
		{
			// Call this method.
			bool _ret = StartGameNot(  );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_EndGameAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = EndGameAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_EndGameNot( Message msg )
		{
			List<NGPlayEndInfo> _Info; UMessageMarshal.Read( msg, out _Info );
			// Call this method.
			bool _ret = EndGameNot( _Info );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_OnWaveStateNot( Message msg )
		{
			int _PlayerState; UMessageMarshal.Read( msg, out _PlayerState );
			int _MissionRand; UMessageMarshal.Read( msg, out _MissionRand );
			List<int> _vecAttack; UMessageMarshal.Read( msg, out _vecAttack );
			// Call this method.
			bool _ret = OnWaveStateNot( _PlayerState, _MissionRand, _vecAttack );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_RequestFrameSyncAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = RequestFrameSyncAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_SystemNoticeNot( Message msg )
		{
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			string _Chat; UMessageMarshal.Read( msg, out _Chat );
			// Call this method.
			bool _ret = SystemNoticeNot( _AccountID, _Chat );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_LeaveRoomAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = LeaveRoomAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_OngoingGameResultNot( Message msg )
		{
			List<NGPlayEndInfo> _vecInfo; UMessageMarshal.Read( msg, out _vecInfo );
			// Call this method.
			bool _ret = OngoingGameResultNot( _vecInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_SystemNoticeNot2( Message msg )
		{
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			int Type; UMessageMarshal.Read( msg, out Type );
			int value; UMessageMarshal.Read( msg, out value );
			// Call this method.
			bool _ret = SystemNoticeNot2( _AccountID, Type, value );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_RoomUserKickAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGRoomInfo _Room; UMessageMarshal.Read( msg, out _Room );
			// Call this method.
			bool _ret = RoomUserKickAck( Error, _Room );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_RoomUserKickNot( Message msg )
		{
			// Call this method.
			bool _ret = RoomUserKickNot(  );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_VRFrameSyncNot( Message msg )
		{
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			List<NGVRFrameSync> _vecSync; UMessageMarshal.Read( msg, out _vecSync );
			// Call this method.
			bool _ret = VRFrameSyncNot( _AccountID, _vecSync );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
	}
}

