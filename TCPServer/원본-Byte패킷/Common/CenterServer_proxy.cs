using System;
using System.Net;
using System.Collections.Generic;


using NGNet;
namespace CenterServerC2S
{
	public class Proxy : NGNet.RmiProxy
	{
		public void GetServerGroupInfoReq( RmiContext rmiContext, string _DeviceID, int _AuthType, int _DeviceType )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _DeviceID );
			UMessageMarshal.Write( msg, _AuthType );
			UMessageMarshal.Write( msg, _DeviceType );
			RmiSend( rmiContext, Common.Rmi_GetServerGroupInfoReq, msg );
		}
		public void GetConnectGameServerInfoReq( RmiContext rmiContext, int _ServerGroupID )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _ServerGroupID );
			RmiSend( rmiContext, Common.Rmi_GetConnectGameServerInfoReq, msg );
		}
		public void CreateAccountInfoReq( RmiContext rmiContext, string _DeviceID, int _AuthType, string _Nation, int _ServerGroupID )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, _DeviceID );
			UMessageMarshal.Write( msg, _AuthType );
			UMessageMarshal.Write( msg, _Nation );
			UMessageMarshal.Write( msg, _ServerGroupID );
			RmiSend( rmiContext, Common.Rmi_CreateAccountInfoReq, msg );
		}
	}
}

namespace CenterServerS2C
{
	public class Proxy : NGNet.RmiProxy
	{
		public void GetServerGroupInfoAck( RmiContext rmiContext, int Error, List<NGServerGroupInfo> _vecServerGroupInfo, List<NGAccountInfo> _vecAccountInfo )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _vecServerGroupInfo );
			UMessageMarshal.Write( msg, _vecAccountInfo );
			RmiSend( rmiContext, Common.Rmi_GetServerGroupInfoAck, msg );
		}
		public void GetConnectGameServerInfoAck( RmiContext rmiContext, int Error, string _HostName, int _Port )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _HostName );
			UMessageMarshal.Write( msg, _Port );
			RmiSend( rmiContext, Common.Rmi_GetConnectGameServerInfoAck, msg );
		}
		public void CreateAccountInfoAck( RmiContext rmiContext, int Error, NGAccountInfo _info )
		{
			Message msg = new Message();
			UMessageMarshal.Write( msg, Error );
			UMessageMarshal.Write( msg, _info );
			RmiSend( rmiContext, Common.Rmi_CreateAccountInfoAck, msg );
		}
	}
}

