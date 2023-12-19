using System;
using System.Collections.Generic;


using NGNet;
namespace CenterServerC2S
{
	public class Common
	{
		// Message ID that replies to each RMI method.
		public const Int32 Rmi_GetServerGroupInfoReq = 10000;
		public const Int32 Rmi_GetConnectGameServerInfoReq = 10001;
		public const Int32 Rmi_CreateAccountInfoReq = 10002;
	}
}

namespace CenterServerS2C
{
	public class Common
	{
		// Message ID that replies to each RMI method.
		public const Int32 Rmi_GetServerGroupInfoAck = 20000;
		public const Int32 Rmi_GetConnectGameServerInfoAck = 20001;
		public const Int32 Rmi_CreateAccountInfoAck = 20002;
	}
}

