using System;
using System.Collections.Generic;


using NGNet;
namespace GameServerC2S
{
	public class Common
	{
		// Message ID that replies to each RMI method.
		public const Int32 Rmi_LogInReq = 30000;
		public const Int32 Rmi_StartMatchReq = 30001;
		public const Int32 Rmi_CancelMatchReq = 30002;
		public const Int32 Rmi_ChangeNicknameReq = 30003;
		public const Int32 Rmi_TournamentStartMatchReq = 30004;
		public const Int32 Rmi_TournamentCancelMatchReq = 30005;
		public const Int32 Rmi_StartChallengeModeReq = 30006;
		public const Int32 Rmi_EndChallengeModeReq = 30007;
		public const Int32 Rmi_GetChallengeTopRankerReq = 30008;
		public const Int32 Rmi_LearnSkillReq = 30009;
		public const Int32 Rmi_DelSkillReq = 30010;
		public const Int32 Rmi_EndEndlessModeReq = 30011;
		public const Int32 Rmi_GetEndLessModeTopRankerReq = 30012;
		public const Int32 Rmi_ChangeProfileInfoReg = 30013;
	}
}

namespace GameServerS2C
{
	public class Common
	{
		// Message ID that replies to each RMI method.
		public const Int32 Rmi_LogInAck = 40000;
		public const Int32 Rmi_StartMatchAck = 40001;
		public const Int32 Rmi_StartMatchNot = 40002;
		public const Int32 Rmi_CancelMatchAck = 40003;
		public const Int32 Rmi_ChangeNicknameAck = 40004;
		public const Int32 Rmi_TournamentStartMatchAck = 40005;
		public const Int32 Rmi_TournamentStartMatchNot = 40006;
		public const Int32 Rmi_TournamentCancelMatchAck = 40007;
		public const Int32 Rmi_StartChallengeModeAck = 40008;
		public const Int32 Rmi_EndChallengeModeAck = 40009;
		public const Int32 Rmi_GetChallengeTopRankgerAck = 40010;
		public const Int32 Rmi_LearnSkillAck = 40011;
		public const Int32 Rmi_DelSkillAck = 40012;
		public const Int32 Rmi_EndEndlessModeAck = 40013;
		public const Int32 Rmi_GetEndLessModeTopRankerAck = 40014;
	}
}

