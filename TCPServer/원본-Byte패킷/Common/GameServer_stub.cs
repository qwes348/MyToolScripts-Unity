using System;
using System.Net;
using System.Collections.Generic;


using NGNet;
namespace GameServerC2S
{
	public class Stub : NGNet.RmiStub
	{
		public delegate bool LogInReqDelegate( Int64 _AccountID, int _AuthType );
		public LogInReqDelegate LogInReq = delegate ( Int64 _AccountID, int _AuthType )
		{
			return false;
		};
		public delegate bool StartMatchReqDelegate(  );
		public StartMatchReqDelegate StartMatchReq = delegate (  )
		{
			return false;
		};
		public delegate bool CancelMatchReqDelegate(  );
		public CancelMatchReqDelegate CancelMatchReq = delegate (  )
		{
			return false;
		};
		public delegate bool ChangeNicknameReqDelegate( string _Nickname );
		public ChangeNicknameReqDelegate ChangeNicknameReq = delegate ( string _Nickname )
		{
			return false;
		};
		public delegate bool TournamentStartMatchReqDelegate(  );
		public TournamentStartMatchReqDelegate TournamentStartMatchReq = delegate (  )
		{
			return false;
		};
		public delegate bool TournamentCancelMatchReqDelegate(  );
		public TournamentCancelMatchReqDelegate TournamentCancelMatchReq = delegate (  )
		{
			return false;
		};
		public delegate bool StartChallengeModeReqDelegate( int _MapID );
		public StartChallengeModeReqDelegate StartChallengeModeReq = delegate ( int _MapID )
		{
			return false;
		};
		public delegate bool EndChallengeModeReqDelegate( bool isClear, NGChallengeModeEndInfo _EndInfo );
		public EndChallengeModeReqDelegate EndChallengeModeReq = delegate ( bool isClear, NGChallengeModeEndInfo _EndInfo )
		{
			return false;
		};
		public delegate bool GetChallengeTopRankerReqDelegate( int MapID );
		public GetChallengeTopRankerReqDelegate GetChallengeTopRankerReq = delegate ( int MapID )
		{
			return false;
		};
		public delegate bool LearnSkillReqDelegate( NGSkillInfo _SkillInfo );
		public LearnSkillReqDelegate LearnSkillReq = delegate ( NGSkillInfo _SkillInfo )
		{
			return false;
		};
		public delegate bool DelSkillReqDelegate( NGSkillInfo _SkillInfo );
		public DelSkillReqDelegate DelSkillReq = delegate ( NGSkillInfo _SkillInfo )
		{
			return false;
		};
		public delegate bool EndEndlessModeReqDelegate( NGEndlessModeEndInfo _EndInfo );
		public EndEndlessModeReqDelegate EndEndlessModeReq = delegate ( NGEndlessModeEndInfo _EndInfo )
		{
			return false;
		};
		public delegate bool GetEndLessModeTopRankerReqDelegate(  );
		public GetEndLessModeTopRankerReqDelegate GetEndLessModeTopRankerReq = delegate (  )
		{
			return false;
		};
		public delegate bool ChangeProfileInfoRegDelegate( NGProfileInfo _profileInfo );
		public ChangeProfileInfoRegDelegate ChangeProfileInfoReg = delegate ( NGProfileInfo _profileInfo )
		{
			return false;
		};
		public override bool ProcessReceivedMessage( Message msg )
		{
			switch( msg.ID )
			{
			case Common.Rmi_LogInReq:
				ProcessReceivedMessage_LogInReq( msg );
				break;
			case Common.Rmi_StartMatchReq:
				ProcessReceivedMessage_StartMatchReq( msg );
				break;
			case Common.Rmi_CancelMatchReq:
				ProcessReceivedMessage_CancelMatchReq( msg );
				break;
			case Common.Rmi_ChangeNicknameReq:
				ProcessReceivedMessage_ChangeNicknameReq( msg );
				break;
			case Common.Rmi_TournamentStartMatchReq:
				ProcessReceivedMessage_TournamentStartMatchReq( msg );
				break;
			case Common.Rmi_TournamentCancelMatchReq:
				ProcessReceivedMessage_TournamentCancelMatchReq( msg );
				break;
			case Common.Rmi_StartChallengeModeReq:
				ProcessReceivedMessage_StartChallengeModeReq( msg );
				break;
			case Common.Rmi_EndChallengeModeReq:
				ProcessReceivedMessage_EndChallengeModeReq( msg );
				break;
			case Common.Rmi_GetChallengeTopRankerReq:
				ProcessReceivedMessage_GetChallengeTopRankerReq( msg );
				break;
			case Common.Rmi_LearnSkillReq:
				ProcessReceivedMessage_LearnSkillReq( msg );
				break;
			case Common.Rmi_DelSkillReq:
				ProcessReceivedMessage_DelSkillReq( msg );
				break;
			case Common.Rmi_EndEndlessModeReq:
				ProcessReceivedMessage_EndEndlessModeReq( msg );
				break;
			case Common.Rmi_GetEndLessModeTopRankerReq:
				ProcessReceivedMessage_GetEndLessModeTopRankerReq( msg );
				break;
			case Common.Rmi_ChangeProfileInfoReg:
				ProcessReceivedMessage_ChangeProfileInfoReg( msg );
				break;
			default: return false;
			}
			return true;
		}
		void ProcessReceivedMessage_LogInReq( Message msg )
		{
			Int64 _AccountID; UMessageMarshal.Read( msg, out _AccountID );
			int _AuthType; UMessageMarshal.Read( msg, out _AuthType );
			// Call this method.
			bool _ret = LogInReq( _AccountID, _AuthType );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartMatchReq( Message msg )
		{
			// Call this method.
			bool _ret = StartMatchReq(  );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_CancelMatchReq( Message msg )
		{
			// Call this method.
			bool _ret = CancelMatchReq(  );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_ChangeNicknameReq( Message msg )
		{
			string _Nickname; UMessageMarshal.Read( msg, out _Nickname );
			// Call this method.
			bool _ret = ChangeNicknameReq( _Nickname );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_TournamentStartMatchReq( Message msg )
		{
			// Call this method.
			bool _ret = TournamentStartMatchReq(  );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_TournamentCancelMatchReq( Message msg )
		{
			// Call this method.
			bool _ret = TournamentCancelMatchReq(  );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartChallengeModeReq( Message msg )
		{
			int _MapID; UMessageMarshal.Read( msg, out _MapID );
			// Call this method.
			bool _ret = StartChallengeModeReq( _MapID );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_EndChallengeModeReq( Message msg )
		{
			bool isClear; UMessageMarshal.Read( msg, out isClear );
			NGChallengeModeEndInfo _EndInfo; UMessageMarshal.Read( msg, out _EndInfo );
			// Call this method.
			bool _ret = EndChallengeModeReq( isClear, _EndInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_GetChallengeTopRankerReq( Message msg )
		{
			int MapID; UMessageMarshal.Read( msg, out MapID );
			// Call this method.
			bool _ret = GetChallengeTopRankerReq( MapID );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_LearnSkillReq( Message msg )
		{
			NGSkillInfo _SkillInfo; UMessageMarshal.Read( msg, out _SkillInfo );
			// Call this method.
			bool _ret = LearnSkillReq( _SkillInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_DelSkillReq( Message msg )
		{
			NGSkillInfo _SkillInfo; UMessageMarshal.Read( msg, out _SkillInfo );
			// Call this method.
			bool _ret = DelSkillReq( _SkillInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_EndEndlessModeReq( Message msg )
		{
			NGEndlessModeEndInfo _EndInfo; UMessageMarshal.Read( msg, out _EndInfo );
			// Call this method.
			bool _ret = EndEndlessModeReq( _EndInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_GetEndLessModeTopRankerReq( Message msg )
		{
			// Call this method.
			bool _ret = GetEndLessModeTopRankerReq(  );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_ChangeProfileInfoReg( Message msg )
		{
			NGProfileInfo _profileInfo; UMessageMarshal.Read( msg, out _profileInfo );
			// Call this method.
			bool _ret = ChangeProfileInfoReg( _profileInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
	}
}

namespace GameServerS2C
{
	public class Stub : NGNet.RmiStub
	{
		public delegate bool LogInAckDelegate( int Error, NGLogInAck _Ack );
		public LogInAckDelegate LogInAck = delegate ( int Error, NGLogInAck _Ack )
		{
			return false;
		};
		public delegate bool StartMatchAckDelegate( int Error );
		public StartMatchAckDelegate StartMatchAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool StartMatchNotDelegate( Int64 _RoomIndex, string _HostName, int _Port );
		public StartMatchNotDelegate StartMatchNot = delegate ( Int64 _RoomIndex, string _HostName, int _Port )
		{
			return false;
		};
		public delegate bool CancelMatchAckDelegate( int Error );
		public CancelMatchAckDelegate CancelMatchAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool ChangeNicknameAckDelegate( int Error, string _Nickname );
		public ChangeNicknameAckDelegate ChangeNicknameAck = delegate ( int Error, string _Nickname )
		{
			return false;
		};
		public delegate bool TournamentStartMatchAckDelegate( int Error );
		public TournamentStartMatchAckDelegate TournamentStartMatchAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool TournamentStartMatchNotDelegate( Int64 _RoomIndex, string _HostName, int _Port );
		public TournamentStartMatchNotDelegate TournamentStartMatchNot = delegate ( Int64 _RoomIndex, string _HostName, int _Port )
		{
			return false;
		};
		public delegate bool TournamentCancelMatchAckDelegate( int Error );
		public TournamentCancelMatchAckDelegate TournamentCancelMatchAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool StartChallengeModeAckDelegate( int Error );
		public StartChallengeModeAckDelegate StartChallengeModeAck = delegate ( int Error )
		{
			return false;
		};
		public delegate bool EndChallengeModeAckDelegate( int Error, NGChallengeModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo );
		public EndChallengeModeAckDelegate EndChallengeModeAck = delegate ( int Error, NGChallengeModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo )
		{
			return false;
		};
		public delegate bool GetChallengeTopRankgerAckDelegate( int Error, List<NGChallengeRanker> vecRanker );
		public GetChallengeTopRankgerAckDelegate GetChallengeTopRankgerAck = delegate ( int Error, List<NGChallengeRanker> vecRanker )
		{
			return false;
		};
		public delegate bool LearnSkillAckDelegate( int Error, NGSkillInfo _SkillInfo );
		public LearnSkillAckDelegate LearnSkillAck = delegate ( int Error, NGSkillInfo _SkillInfo )
		{
			return false;
		};
		public delegate bool DelSkillAckDelegate( int Error, NGSkillInfo _SkillInfo );
		public DelSkillAckDelegate DelSkillAck = delegate ( int Error, NGSkillInfo _SkillInfo )
		{
			return false;
		};
		public delegate bool EndEndlessModeAckDelegate( int Error, NGEndlessModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo );
		public EndEndlessModeAckDelegate EndEndlessModeAck = delegate ( int Error, NGEndlessModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo )
		{
			return false;
		};
		public delegate bool GetEndLessModeTopRankerAckDelegate( int Error, List<NGEndlessModeRanker> vecRanker );
		public GetEndLessModeTopRankerAckDelegate GetEndLessModeTopRankerAck = delegate ( int Error, List<NGEndlessModeRanker> vecRanker )
		{
			return false;
		};
		public override bool ProcessReceivedMessage( Message msg )
		{
			switch( msg.ID )
			{
			case Common.Rmi_LogInAck:
				ProcessReceivedMessage_LogInAck( msg );
				break;
			case Common.Rmi_StartMatchAck:
				ProcessReceivedMessage_StartMatchAck( msg );
				break;
			case Common.Rmi_StartMatchNot:
				ProcessReceivedMessage_StartMatchNot( msg );
				break;
			case Common.Rmi_CancelMatchAck:
				ProcessReceivedMessage_CancelMatchAck( msg );
				break;
			case Common.Rmi_ChangeNicknameAck:
				ProcessReceivedMessage_ChangeNicknameAck( msg );
				break;
			case Common.Rmi_TournamentStartMatchAck:
				ProcessReceivedMessage_TournamentStartMatchAck( msg );
				break;
			case Common.Rmi_TournamentStartMatchNot:
				ProcessReceivedMessage_TournamentStartMatchNot( msg );
				break;
			case Common.Rmi_TournamentCancelMatchAck:
				ProcessReceivedMessage_TournamentCancelMatchAck( msg );
				break;
			case Common.Rmi_StartChallengeModeAck:
				ProcessReceivedMessage_StartChallengeModeAck( msg );
				break;
			case Common.Rmi_EndChallengeModeAck:
				ProcessReceivedMessage_EndChallengeModeAck( msg );
				break;
			case Common.Rmi_GetChallengeTopRankgerAck:
				ProcessReceivedMessage_GetChallengeTopRankgerAck( msg );
				break;
			case Common.Rmi_LearnSkillAck:
				ProcessReceivedMessage_LearnSkillAck( msg );
				break;
			case Common.Rmi_DelSkillAck:
				ProcessReceivedMessage_DelSkillAck( msg );
				break;
			case Common.Rmi_EndEndlessModeAck:
				ProcessReceivedMessage_EndEndlessModeAck( msg );
				break;
			case Common.Rmi_GetEndLessModeTopRankerAck:
				ProcessReceivedMessage_GetEndLessModeTopRankerAck( msg );
				break;
			default: return false;
			}
			return true;
		}
		void ProcessReceivedMessage_LogInAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGLogInAck _Ack; UMessageMarshal.Read( msg, out _Ack );
			// Call this method.
			bool _ret = LogInAck( Error, _Ack );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartMatchAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = StartMatchAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartMatchNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			string _HostName; UMessageMarshal.Read( msg, out _HostName );
			int _Port; UMessageMarshal.Read( msg, out _Port );
			// Call this method.
			bool _ret = StartMatchNot( _RoomIndex, _HostName, _Port );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_CancelMatchAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = CancelMatchAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_ChangeNicknameAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			string _Nickname; UMessageMarshal.Read( msg, out _Nickname );
			// Call this method.
			bool _ret = ChangeNicknameAck( Error, _Nickname );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_TournamentStartMatchAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = TournamentStartMatchAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_TournamentStartMatchNot( Message msg )
		{
			Int64 _RoomIndex; UMessageMarshal.Read( msg, out _RoomIndex );
			string _HostName; UMessageMarshal.Read( msg, out _HostName );
			int _Port; UMessageMarshal.Read( msg, out _Port );
			// Call this method.
			bool _ret = TournamentStartMatchNot( _RoomIndex, _HostName, _Port );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_TournamentCancelMatchAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = TournamentCancelMatchAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_StartChallengeModeAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			// Call this method.
			bool _ret = StartChallengeModeAck( Error );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_EndChallengeModeAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGChallengeModeEndInfo _EndInfo; UMessageMarshal.Read( msg, out _EndInfo );
			List<NGCollectionInfo> _CollectionInfo; UMessageMarshal.Read( msg, out _CollectionInfo );
			// Call this method.
			bool _ret = EndChallengeModeAck( Error, _EndInfo, _CollectionInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_GetChallengeTopRankgerAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			List<NGChallengeRanker> vecRanker; UMessageMarshal.Read( msg, out vecRanker );
			// Call this method.
			bool _ret = GetChallengeTopRankgerAck( Error, vecRanker );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_LearnSkillAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGSkillInfo _SkillInfo; UMessageMarshal.Read( msg, out _SkillInfo );
			// Call this method.
			bool _ret = LearnSkillAck( Error, _SkillInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_DelSkillAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGSkillInfo _SkillInfo; UMessageMarshal.Read( msg, out _SkillInfo );
			// Call this method.
			bool _ret = DelSkillAck( Error, _SkillInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_EndEndlessModeAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGEndlessModeEndInfo _EndInfo; UMessageMarshal.Read( msg, out _EndInfo );
			List<NGCollectionInfo> _CollectionInfo; UMessageMarshal.Read( msg, out _CollectionInfo );
			// Call this method.
			bool _ret = EndEndlessModeAck( Error, _EndInfo, _CollectionInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_GetEndLessModeTopRankerAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			List<NGEndlessModeRanker> vecRanker; UMessageMarshal.Read( msg, out vecRanker );
			// Call this method.
			bool _ret = GetEndLessModeTopRankerAck( Error, vecRanker );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
	}
}

