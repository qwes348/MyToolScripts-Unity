using System;
using System.Net;
using System.Collections.Generic;


using NGNet;
namespace GameServerC2S
{
	public class Proxy : NGNet.RmiProxy
	{
		public void LogInReq( RmiContext rmiContext, Int64 _AccountID, int _AuthType )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _AccountID );
			UMessageMarshal.Write( msg, _AuthType );
			RmiSend( rmiContext, Common.Rmi_LogInReq, msg );
		}
		public void StartMatchReq( RmiContext rmiContext )
		{
			Message msg = new Message();
			RmiSend( rmiContext, Common.Rmi_StartMatchReq, msg );
		}
		public void CancelMatchReq( RmiContext rmiContext )
		{
			Message msg = new Message();
			RmiSend( rmiContext, Common.Rmi_CancelMatchReq, msg );
		}
		public void ChangeNicknameReq( RmiContext rmiContext, string _Nickname )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _Nickname );
			RmiSend( rmiContext, Common.Rmi_ChangeNicknameReq, msg );
		}
		public void TournamentStartMatchReq( RmiContext rmiContext )
		{
			Message msg = new Message();
			RmiSend( rmiContext, Common.Rmi_TournamentStartMatchReq, msg );
		}
		public void TournamentCancelMatchReq( RmiContext rmiContext )
		{
			Message msg = new Message();
			RmiSend( rmiContext, Common.Rmi_TournamentCancelMatchReq, msg );
		}
		public void StartChallengeModeReq( RmiContext rmiContext, int _MapID )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _MapID );
			RmiSend( rmiContext, Common.Rmi_StartChallengeModeReq, msg );
		}
		public void EndChallengeModeReq( RmiContext rmiContext, bool isClear, NGChallengeModeEndInfo _EndInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, isClear );
			UMessageMarshal.Write( msg, _EndInfo );
			RmiSend( rmiContext, Common.Rmi_EndChallengeModeReq, msg );
		}
		public void GetChallengeTopRankerReq( RmiContext rmiContext, int MapID )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, MapID );
			RmiSend( rmiContext, Common.Rmi_GetChallengeTopRankerReq, msg );
		}
		public void LearnSkillReq( RmiContext rmiContext, NGSkillInfo _SkillInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _SkillInfo );
			RmiSend( rmiContext, Common.Rmi_LearnSkillReq, msg );
		}
		public void DelSkillReq( RmiContext rmiContext, NGSkillInfo _SkillInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _SkillInfo );
			RmiSend( rmiContext, Common.Rmi_DelSkillReq, msg );
		}
		public void EndEndlessModeReq( RmiContext rmiContext, NGEndlessModeEndInfo _EndInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _EndInfo );
			RmiSend( rmiContext, Common.Rmi_EndEndlessModeReq, msg );
		}
		public void GetEndLessModeTopRankerReq( RmiContext rmiContext )
		{
			Message msg = new Message();
			RmiSend( rmiContext, Common.Rmi_GetEndLessModeTopRankerReq, msg );
		}
		public void ChangeProfileInfoReg( RmiContext rmiContext, NGProfileInfo _profileInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _profileInfo );
			RmiSend( rmiContext, Common.Rmi_ChangeProfileInfoReg, msg );
		}
	}
}

namespace GameServerS2C
{
	public class Proxy : NGNet.RmiProxy
	{
		public void LogInAck( RmiContext rmiContext, int Error, NGLogInAck _Ack )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _Ack );
			RmiSend( rmiContext, Common.Rmi_LogInAck, msg );
		}
		public void StartMatchAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_StartMatchAck, msg );
		}
		public void StartMatchNot( RmiContext rmiContext, Int64 _RoomIndex, string _HostName, int _Port )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _HostName );
			UMessageMarshal.Write( msg, _Port );
			RmiSend( rmiContext, Common.Rmi_StartMatchNot, msg );
		}
		public void CancelMatchAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_CancelMatchAck, msg );
		}
		public void ChangeNicknameAck( RmiContext rmiContext, int Error, string _Nickname )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _Nickname );
			RmiSend( rmiContext, Common.Rmi_ChangeNicknameAck, msg );
		}
		public void TournamentStartMatchAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_TournamentStartMatchAck, msg );
		}
		public void TournamentStartMatchNot( RmiContext rmiContext, Int64 _RoomIndex, string _HostName, int _Port )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _RoomIndex );
			UMessageMarshal.Write( msg, _HostName );
			UMessageMarshal.Write( msg, _Port );
			RmiSend( rmiContext, Common.Rmi_TournamentStartMatchNot, msg );
		}
		public void TournamentCancelMatchAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_TournamentCancelMatchAck, msg );
		}
		public void StartChallengeModeAck( RmiContext rmiContext, int Error )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			RmiSend( rmiContext, Common.Rmi_StartChallengeModeAck, msg );
		}
		public void EndChallengeModeAck( RmiContext rmiContext, int Error, NGChallengeModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _EndInfo );
			UMessageMarshal.Write( msg, _CollectionInfo );
			RmiSend( rmiContext, Common.Rmi_EndChallengeModeAck, msg );
		}
		public void GetChallengeTopRankgerAck( RmiContext rmiContext, int Error, List<NGChallengeRanker> vecRanker )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, vecRanker );
			RmiSend( rmiContext, Common.Rmi_GetChallengeTopRankgerAck, msg );
		}
		public void LearnSkillAck( RmiContext rmiContext, int Error, NGSkillInfo _SkillInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _SkillInfo );
			RmiSend( rmiContext, Common.Rmi_LearnSkillAck, msg );
		}
		public void DelSkillAck( RmiContext rmiContext, int Error, NGSkillInfo _SkillInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _SkillInfo );
			RmiSend( rmiContext, Common.Rmi_DelSkillAck, msg );
		}
		public void EndEndlessModeAck( RmiContext rmiContext, int Error, NGEndlessModeEndInfo _EndInfo, List<NGCollectionInfo> _CollectionInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _EndInfo );
			UMessageMarshal.Write( msg, _CollectionInfo );
			RmiSend( rmiContext, Common.Rmi_EndEndlessModeAck, msg );
		}
		public void GetEndLessModeTopRankerAck( RmiContext rmiContext, int Error, List<NGEndlessModeRanker> vecRanker )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, vecRanker );
			RmiSend( rmiContext, Common.Rmi_GetEndLessModeTopRankerAck, msg );
		}
	}
}

