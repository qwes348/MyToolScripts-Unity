using System;
using System.Net;
using System.Collections.Generic;


using NGNet;
namespace CenterServerC2S
{
	public class Stub : NGNet.RmiStub
	{
		public delegate bool GetServerGroupInfoReqDelegate( string _DeviceID, int _AuthType, int _DeviceType );
		public GetServerGroupInfoReqDelegate GetServerGroupInfoReq = delegate ( string _DeviceID, int _AuthType, int _DeviceType )
		{
			return false;
		};
		public delegate bool GetConnectGameServerInfoReqDelegate( int _ServerGroupID );
		public GetConnectGameServerInfoReqDelegate GetConnectGameServerInfoReq = delegate ( int _ServerGroupID )
		{
			return false;
		};
		public delegate bool CreateAccountInfoReqDelegate( string _DeviceID, int _AuthType, string _Nation, int _ServerGroupID );
		public CreateAccountInfoReqDelegate CreateAccountInfoReq = delegate ( string _DeviceID, int _AuthType, string _Nation, int _ServerGroupID )
		{
			return false;
		};
		public override bool ProcessReceivedMessage( Message msg )
		{
			switch( msg.ID )
			{
			case Common.Rmi_GetServerGroupInfoReq:
				ProcessReceivedMessage_GetServerGroupInfoReq( msg );
				break;
			case Common.Rmi_GetConnectGameServerInfoReq:
				ProcessReceivedMessage_GetConnectGameServerInfoReq( msg );
				break;
			case Common.Rmi_CreateAccountInfoReq:
				ProcessReceivedMessage_CreateAccountInfoReq( msg );
				break;
			default: return false;
			}
			return true;
		}
		void ProcessReceivedMessage_GetServerGroupInfoReq( Message msg )
		{
			string _DeviceID; UMessageMarshal.Read( msg, out _DeviceID );
			int _AuthType; UMessageMarshal.Read( msg, out _AuthType );
			int _DeviceType; UMessageMarshal.Read( msg, out _DeviceType );
			// Call this method.
			bool _ret = GetServerGroupInfoReq( _DeviceID, _AuthType, _DeviceType );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_GetConnectGameServerInfoReq( Message msg )
		{
			int _ServerGroupID; UMessageMarshal.Read( msg, out _ServerGroupID );
			// Call this method.
			bool _ret = GetConnectGameServerInfoReq( _ServerGroupID );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_CreateAccountInfoReq( Message msg )
		{
			string _DeviceID; UMessageMarshal.Read( msg, out _DeviceID );
			int _AuthType; UMessageMarshal.Read( msg, out _AuthType );
			string _Nation; UMessageMarshal.Read( msg, out _Nation );
			int _ServerGroupID; UMessageMarshal.Read( msg, out _ServerGroupID );
			// Call this method.
			bool _ret = CreateAccountInfoReq( _DeviceID, _AuthType, _Nation, _ServerGroupID );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
	}
}

namespace CenterServerS2C
{
	public class Stub : NGNet.RmiStub
	{
		public delegate bool GetServerGroupInfoAckDelegate( int Error, List<NGServerGroupInfo> _vecServerGroupInfo, List<NGAccountInfo> _vecAccountInfo );
		public GetServerGroupInfoAckDelegate GetServerGroupInfoAck = delegate ( int Error, List<NGServerGroupInfo> _vecServerGroupInfo, List<NGAccountInfo> _vecAccountInfo )
		{
			return false;
		};
		public delegate bool GetConnectGameServerInfoAckDelegate( int Error, string _HostName, int _Port );
		public GetConnectGameServerInfoAckDelegate GetConnectGameServerInfoAck = delegate ( int Error, string _HostName, int _Port )
		{
			return false;
		};
		public delegate bool CreateAccountInfoAckDelegate( int Error, NGAccountInfo _info );
		public CreateAccountInfoAckDelegate CreateAccountInfoAck = delegate ( int Error, NGAccountInfo _info )
		{
			return false;
		};
		public override bool ProcessReceivedMessage( Message msg )
		{
			switch( msg.ID )
			{
			case Common.Rmi_GetServerGroupInfoAck:
				ProcessReceivedMessage_GetServerGroupInfoAck( msg );
				break;
			case Common.Rmi_GetConnectGameServerInfoAck:
				ProcessReceivedMessage_GetConnectGameServerInfoAck( msg );
				break;
			case Common.Rmi_CreateAccountInfoAck:
				ProcessReceivedMessage_CreateAccountInfoAck( msg );
				break;
			default: return false;
			}
			return true;
		}
		void ProcessReceivedMessage_GetServerGroupInfoAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			List<NGServerGroupInfo> _vecServerGroupInfo; UMessageMarshal.Read( msg, out _vecServerGroupInfo );
			List<NGAccountInfo> _vecAccountInfo; UMessageMarshal.Read( msg, out _vecAccountInfo );
			// Call this method.
			bool _ret = GetServerGroupInfoAck( Error, _vecServerGroupInfo, _vecAccountInfo );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_GetConnectGameServerInfoAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			string _HostName; UMessageMarshal.Read( msg, out _HostName );
			int _Port; UMessageMarshal.Read( msg, out _Port );
			// Call this method.
			bool _ret = GetConnectGameServerInfoAck( Error, _HostName, _Port );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
		void ProcessReceivedMessage_CreateAccountInfoAck( Message msg )
		{
			int Error; UMessageMarshal.Read( msg, out Error );
			NGAccountInfo _info; UMessageMarshal.Read( msg, out _info );
			// Call this method.
			bool _ret = CreateAccountInfoAck( Error, _info );
			if( _ret == false )
				Console.WriteLine( "Error: RMI function that a user did not create has been called." );
		}
	}
}

