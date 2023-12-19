using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 

	/*
	* 사용자 정의 데이터형 Read, Write 만드는 곳
	* 사용자 정의 List, Dictionary 추가 시에는 Messagemarshal 의 샘플을 보고 추가하자
	*/

using NGNet;
public class UMessageMarshal : NGNet.MessageMarshal
{
	/* 
	NGPairInt2 packet
	*/ 
	public static void Read( Message msg, out NGPairInt2 v )
	{
		v = new NGPairInt2();
		Read( msg, out v.first );
		Read( msg, out v.second );
	}
	public static void Write( Message msg, NGPairInt2 v )
	{
		Write( msg, v.first );
		Write( msg, v.second );
	}
	public static void Read( Message msg, out List<NGPairInt2> values )
	{
		values = new List<NGPairInt2>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGPairInt2 value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGPairInt2> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	Vec2 packet
	*/ 
	public static void Read( Message msg, out Vec2 v )
	{
		v = new Vec2();
		Read( msg, out v.x );
		Read( msg, out v.y );
	}
	public static void Write( Message msg, Vec2 v )
	{
		Write( msg, v.x );
		Write( msg, v.y );
	}
	public static void Read( Message msg, out List<Vec2> values )
	{
		values = new List<Vec2>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			Vec2 value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<Vec2> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	FarmClientInfo packet
	*/ 
	public static void Read( Message msg, out FarmClientInfo v )
	{
		v = new FarmClientInfo();
		Read( msg, out v.serverMode );
		Read( msg, out v.gamerCount );
		Read( msg, out v.HostName );
		Read( msg, out v.Port );
		Read( msg, out v.ServerGroupID );
	}
	public static void Write( Message msg, FarmClientInfo v )
	{
		Write( msg, v.serverMode );
		Write( msg, v.gamerCount );
		Write( msg, v.HostName );
		Write( msg, v.Port );
		Write( msg, v.ServerGroupID );
	}
	public static void Read( Message msg, out List<FarmClientInfo> values )
	{
		values = new List<FarmClientInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			FarmClientInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<FarmClientInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGServerGroupInfo packet
	*/ 
	public static void Read( Message msg, out NGServerGroupInfo v )
	{
		v = new NGServerGroupInfo();
		Read( msg, out v.GroupID );
		Read( msg, out v.CenterServerIp );
		Read( msg, out v.CenterServerPort );
		Read( msg, out v.ServerName );
	}
	public static void Write( Message msg, NGServerGroupInfo v )
	{
		Write( msg, v.GroupID );
		Write( msg, v.CenterServerIp );
		Write( msg, v.CenterServerPort );
		Write( msg, v.ServerName );
	}
	public static void Read( Message msg, out List<NGServerGroupInfo> values )
	{
		values = new List<NGServerGroupInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGServerGroupInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGServerGroupInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGAccountInfo packet
	*/ 
	public static void Read( Message msg, out NGAccountInfo v )
	{
		v = new NGAccountInfo();
		Read( msg, out v.DeviceID );
		Read( msg, out v.AccountID );
		Read( msg, out v.AuthType );
		Read( msg, out v.ServerGroup );
		Read( msg, out v.RegDate );
		Read( msg, out v.LastLogInDate );
	}
	public static void Write( Message msg, NGAccountInfo v )
	{
		Write( msg, v.DeviceID );
		Write( msg, v.AccountID );
		Write( msg, v.AuthType );
		Write( msg, v.ServerGroup );
		Write( msg, v.RegDate );
		Write( msg, v.LastLogInDate );
	}
	public static void Read( Message msg, out List<NGAccountInfo> values )
	{
		values = new List<NGAccountInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGAccountInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGAccountInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGUserInfo packet
	*/ 
	public static void Read( Message msg, out NGUserInfo v )
	{
		v = new NGUserInfo();
		Read( msg, out v.NickName );
		Read( msg, out v.Nation );
		Read( msg, out v.RegDate );
		Read( msg, out v.LastLoginDate );
		Read( msg, out v.AccountID );
	}
	public static void Write( Message msg, NGUserInfo v )
	{
		Write( msg, v.NickName );
		Write( msg, v.Nation );
		Write( msg, v.RegDate );
		Write( msg, v.LastLoginDate );
		Write( msg, v.AccountID );
	}
	public static void Read( Message msg, out List<NGUserInfo> values )
	{
		values = new List<NGUserInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGUserInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGUserInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	SecretMissionConditionSync packet
	*/ 
	public static void Read( Message msg, out SecretMissionConditionSync v )
	{
		v = new SecretMissionConditionSync();
		Read( msg, out v.NameID );
		Read( msg, out v.DescID );
		Read( msg, out v.type );
		Read( msg, out v.score );
		Read( msg, out v.ratio );
		Read( msg, out v.tower );
		Read( msg, out v.count );
		Read( msg, out v.condition );
	}
	public static void Write( Message msg, SecretMissionConditionSync v )
	{
		Write( msg, v.NameID );
		Write( msg, v.DescID );
		Write( msg, v.type );
		Write( msg, v.score );
		Write( msg, v.ratio );
		Write( msg, v.tower );
		Write( msg, v.count );
		Write( msg, v.condition );
	}
	public static void Read( Message msg, out List<SecretMissionConditionSync> values )
	{
		values = new List<SecretMissionConditionSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			SecretMissionConditionSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<SecretMissionConditionSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	SecretMissionRewardScriptInfo packet
	*/ 
	public static void Read( Message msg, out SecretMissionRewardScriptInfo v )
	{
		v = new SecretMissionRewardScriptInfo();
		Read( msg, out v.score );
		Read( msg, out v.ratio );
		Read( msg, out v.reward );
		Read( msg, out v.count );
	}
	public static void Write( Message msg, SecretMissionRewardScriptInfo v )
	{
		Write( msg, v.score );
		Write( msg, v.ratio );
		Write( msg, v.reward );
		Write( msg, v.count );
	}
	public static void Read( Message msg, out List<SecretMissionRewardScriptInfo> values )
	{
		values = new List<SecretMissionRewardScriptInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			SecretMissionRewardScriptInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<SecretMissionRewardScriptInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	SecretMissionSync packet
	*/ 
	public static void Read( Message msg, out SecretMissionSync v )
	{
		v = new SecretMissionSync();
		Read( msg, out v.missionCondition );
		Read( msg, out v.rewardInfo );
		Read( msg, out v.clearMission );
		Read( msg, out v.progress );
	}
	public static void Write( Message msg, SecretMissionSync v )
	{
		Write( msg, v.missionCondition );
		Write( msg, v.rewardInfo );
		Write( msg, v.clearMission );
		Write( msg, v.progress );
	}
	public static void Read( Message msg, out List<SecretMissionSync> values )
	{
		values = new List<SecretMissionSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			SecretMissionSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<SecretMissionSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGMonsterObjectSync packet
	*/ 
	public static void Read( Message msg, out NGMonsterObjectSync v )
	{
		v = new NGMonsterObjectSync();
		Read( msg, out v.uid );
		Read( msg, out v.monsterID );
		Read( msg, out v.lifeStat );
		Read( msg, out v.pathIndex );
		Read( msg, out v.pos );
		Read( msg, out v.hp );
	}
	public static void Write( Message msg, NGMonsterObjectSync v )
	{
		Write( msg, v.uid );
		Write( msg, v.monsterID );
		Write( msg, v.lifeStat );
		Write( msg, v.pathIndex );
		Write( msg, v.pos );
		Write( msg, v.hp );
	}
	public static void Read( Message msg, out List<NGMonsterObjectSync> values )
	{
		values = new List<NGMonsterObjectSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGMonsterObjectSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGMonsterObjectSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGTowerObjectSync packet
	*/ 
	public static void Read( Message msg, out NGTowerObjectSync v )
	{
		v = new NGTowerObjectSync();
		Read( msg, out v.uid );
		Read( msg, out v.towerID );
		Read( msg, out v.lifeStat );
		Read( msg, out v.areaIndex );
		Read( msg, out v.killCount );
		Read( msg, out v.attackDelay );
		Read( msg, out v.skillCoolDown );
		Read( msg, out v.useOnceSkill );
	}
	public static void Write( Message msg, NGTowerObjectSync v )
	{
		Write( msg, v.uid );
		Write( msg, v.towerID );
		Write( msg, v.lifeStat );
		Write( msg, v.areaIndex );
		Write( msg, v.killCount );
		Write( msg, v.attackDelay );
		Write( msg, v.skillCoolDown );
		Write( msg, v.useOnceSkill );
	}
	public static void Read( Message msg, out List<NGTowerObjectSync> values )
	{
		values = new List<NGTowerObjectSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGTowerObjectSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGTowerObjectSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGProjectileObjectSync packet
	*/ 
	public static void Read( Message msg, out NGProjectileObjectSync v )
	{
		v = new NGProjectileObjectSync();
		Read( msg, out v.uid );
		Read( msg, out v.projectileID );
		Read( msg, out v.lifeStat );
		Read( msg, out v.pos );
		Read( msg, out v.attackEffect );
		Read( msg, out v.damage );
		Read( msg, out v.upgradeDamage );
		Read( msg, out v.targetMonster );
		Read( msg, out v.parentTower );
	}
	public static void Write( Message msg, NGProjectileObjectSync v )
	{
		Write( msg, v.uid );
		Write( msg, v.projectileID );
		Write( msg, v.lifeStat );
		Write( msg, v.pos );
		Write( msg, v.attackEffect );
		Write( msg, v.damage );
		Write( msg, v.upgradeDamage );
		Write( msg, v.targetMonster );
		Write( msg, v.parentTower );
	}
	public static void Read( Message msg, out List<NGProjectileObjectSync> values )
	{
		values = new List<NGProjectileObjectSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGProjectileObjectSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGProjectileObjectSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGBaseFrameSync packet
	*/ 
	public static void Read( Message msg, out NGBaseFrameSync v )
	{
		v = new NGBaseFrameSync();
		Read( msg, out v.NowTurn );
		Read( msg, out v.playerState );
		Read( msg, out v.lifeCount );
		Read( msg, out v.money );
		Read( msg, out v.rareTicket );
		Read( msg, out v.uniqueTicket );
		Read( msg, out v.epicTicket );
		Read( msg, out v.monsterCount );
		Read( msg, out v.maxMonsterCount );
		Read( msg, out v.towerScore );
		Read( msg, out v.attackMonster1Count );
		Read( msg, out v.attackMonster2Count );
		Read( msg, out v.attackMonster3Count );
		Read( msg, out v.userAttackPoint );
		Read( msg, out v.userKillPoint );
		Read( msg, out v.useTowerScore );
		Read( msg, out v.gold );
		Read( msg, out v.epicTowerCount );
		Read( msg, out v.missionCounts );
	}
	public static void Write( Message msg, NGBaseFrameSync v )
	{
		Write( msg, v.NowTurn );
		Write( msg, v.playerState );
		Write( msg, v.lifeCount );
		Write( msg, v.money );
		Write( msg, v.rareTicket );
		Write( msg, v.uniqueTicket );
		Write( msg, v.epicTicket );
		Write( msg, v.monsterCount );
		Write( msg, v.maxMonsterCount );
		Write( msg, v.towerScore );
		Write( msg, v.attackMonster1Count );
		Write( msg, v.attackMonster2Count );
		Write( msg, v.attackMonster3Count );
		Write( msg, v.userAttackPoint );
		Write( msg, v.userKillPoint );
		Write( msg, v.useTowerScore );
		Write( msg, v.gold );
		Write( msg, v.epicTowerCount );
		Write( msg, v.missionCounts );
	}
	public static void Read( Message msg, out List<NGBaseFrameSync> values )
	{
		values = new List<NGBaseFrameSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGBaseFrameSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGBaseFrameSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGFrameSync packet
	*/ 
	public static void Read( Message msg, out NGFrameSync v )
	{
		v = new NGFrameSync();
		Read( msg, out v.NowTurn );
		Read( msg, out v.playerState );
		Read( msg, out v.lifeCount );
		Read( msg, out v.money );
		Read( msg, out v.rareTicket );
		Read( msg, out v.uniqueTicket );
		Read( msg, out v.epicTicket );
		Read( msg, out v.monsterCount );
		Read( msg, out v.maxMonsterCount );
		Read( msg, out v.towerScore );
		Read( msg, out v.attackMonster1Count );
		Read( msg, out v.attackMonster2Count );
		Read( msg, out v.attackMonster3Count );
		Read( msg, out v.userAttackPoint );
		Read( msg, out v.userKillPoint );
		Read( msg, out v.useTowerScore );
		Read( msg, out v.gold );
		Read( msg, out v.epicTowerCount );
		Read( msg, out v.missionCounts );
		Read( msg, out v.AccountID );
		Read( msg, out v.upgradeCount );
		Read( msg, out v.personalMissionCount );
		Read( msg, out v.vecSecretMissionInfo );
		Read( msg, out v.vecMonsterSync );
		Read( msg, out v.vecTowerSync );
		Read( msg, out v.vecProjectileSync );
	}
	public static void Write( Message msg, NGFrameSync v )
	{
		Write( msg, v.NowTurn );
		Write( msg, v.playerState );
		Write( msg, v.lifeCount );
		Write( msg, v.money );
		Write( msg, v.rareTicket );
		Write( msg, v.uniqueTicket );
		Write( msg, v.epicTicket );
		Write( msg, v.monsterCount );
		Write( msg, v.maxMonsterCount );
		Write( msg, v.towerScore );
		Write( msg, v.attackMonster1Count );
		Write( msg, v.attackMonster2Count );
		Write( msg, v.attackMonster3Count );
		Write( msg, v.userAttackPoint );
		Write( msg, v.userKillPoint );
		Write( msg, v.useTowerScore );
		Write( msg, v.gold );
		Write( msg, v.epicTowerCount );
		Write( msg, v.missionCounts );
		Write( msg, v.AccountID );
		Write( msg, v.upgradeCount );
		Write( msg, v.personalMissionCount );
		Write( msg, v.vecSecretMissionInfo );
		Write( msg, v.vecMonsterSync );
		Write( msg, v.vecTowerSync );
		Write( msg, v.vecProjectileSync );
	}
	public static void Read( Message msg, out List<NGFrameSync> values )
	{
		values = new List<NGFrameSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGFrameSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGFrameSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGVRBaseFrameSync packet
	*/ 
	public static void Read( Message msg, out NGVRBaseFrameSync v )
	{
		v = new NGVRBaseFrameSync();
		Read( msg, out v.NowTurn );
		Read( msg, out v.playerState );
		Read( msg, out v.lifeCount );
		Read( msg, out v.money );
		Read( msg, out v.rareTicket );
		Read( msg, out v.uniqueTicket );
		Read( msg, out v.epicTicket );
		Read( msg, out v.monsterCount );
		Read( msg, out v.maxMonsterCount );
		Read( msg, out v.towerScore );
		Read( msg, out v.attackMonster1Count );
		Read( msg, out v.attackMonster2Count );
		Read( msg, out v.attackMonster3Count );
		Read( msg, out v.userAttackPoint );
		Read( msg, out v.userKillPoint );
		Read( msg, out v.firstMissionCount );
		Read( msg, out v.secondMissionCount );
		Read( msg, out v.thirdMissionCount );
	}
	public static void Write( Message msg, NGVRBaseFrameSync v )
	{
		Write( msg, v.NowTurn );
		Write( msg, v.playerState );
		Write( msg, v.lifeCount );
		Write( msg, v.money );
		Write( msg, v.rareTicket );
		Write( msg, v.uniqueTicket );
		Write( msg, v.epicTicket );
		Write( msg, v.monsterCount );
		Write( msg, v.maxMonsterCount );
		Write( msg, v.towerScore );
		Write( msg, v.attackMonster1Count );
		Write( msg, v.attackMonster2Count );
		Write( msg, v.attackMonster3Count );
		Write( msg, v.userAttackPoint );
		Write( msg, v.userKillPoint );
		Write( msg, v.firstMissionCount );
		Write( msg, v.secondMissionCount );
		Write( msg, v.thirdMissionCount );
	}
	public static void Read( Message msg, out List<NGVRBaseFrameSync> values )
	{
		values = new List<NGVRBaseFrameSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGVRBaseFrameSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGVRBaseFrameSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGVRFrameSync packet
	*/ 
	public static void Read( Message msg, out NGVRFrameSync v )
	{
		v = new NGVRFrameSync();
		Read( msg, out v.NowTurn );
		Read( msg, out v.playerState );
		Read( msg, out v.lifeCount );
		Read( msg, out v.money );
		Read( msg, out v.rareTicket );
		Read( msg, out v.uniqueTicket );
		Read( msg, out v.epicTicket );
		Read( msg, out v.monsterCount );
		Read( msg, out v.maxMonsterCount );
		Read( msg, out v.towerScore );
		Read( msg, out v.attackMonster1Count );
		Read( msg, out v.attackMonster2Count );
		Read( msg, out v.attackMonster3Count );
		Read( msg, out v.userAttackPoint );
		Read( msg, out v.userKillPoint );
		Read( msg, out v.firstMissionCount );
		Read( msg, out v.secondMissionCount );
		Read( msg, out v.thirdMissionCount );
		Read( msg, out v.upgradeCount );
		Read( msg, out v.personalMissionCount );
		Read( msg, out v.vecSecretMissionInfo );
		Read( msg, out v.vecMonsterSync );
		Read( msg, out v.vecTowerSync );
		Read( msg, out v.vecProjectileSync );
	}
	public static void Write( Message msg, NGVRFrameSync v )
	{
		Write( msg, v.NowTurn );
		Write( msg, v.playerState );
		Write( msg, v.lifeCount );
		Write( msg, v.money );
		Write( msg, v.rareTicket );
		Write( msg, v.uniqueTicket );
		Write( msg, v.epicTicket );
		Write( msg, v.monsterCount );
		Write( msg, v.maxMonsterCount );
		Write( msg, v.towerScore );
		Write( msg, v.attackMonster1Count );
		Write( msg, v.attackMonster2Count );
		Write( msg, v.attackMonster3Count );
		Write( msg, v.userAttackPoint );
		Write( msg, v.userKillPoint );
		Write( msg, v.firstMissionCount );
		Write( msg, v.secondMissionCount );
		Write( msg, v.thirdMissionCount );
		Write( msg, v.upgradeCount );
		Write( msg, v.personalMissionCount );
		Write( msg, v.vecSecretMissionInfo );
		Write( msg, v.vecMonsterSync );
		Write( msg, v.vecTowerSync );
		Write( msg, v.vecProjectileSync );
	}
	public static void Read( Message msg, out List<NGVRFrameSync> values )
	{
		values = new List<NGVRFrameSync>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGVRFrameSync value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGVRFrameSync> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGRoomPlayerInfo packet
	*/ 
	public static void Read( Message msg, out NGRoomPlayerInfo v )
	{
		v = new NGRoomPlayerInfo();
		Read( msg, out v.AccountID );
		Read( msg, out v.playerState );
		Read( msg, out v.Index );
		Read( msg, out v.nick );
		Read( msg, out v.remote );
		Read( msg, out v.isOnline );
	}
	public static void Write( Message msg, NGRoomPlayerInfo v )
	{
		Write( msg, v.AccountID );
		Write( msg, v.playerState );
		Write( msg, v.Index );
		Write( msg, v.nick );
		Write( msg, v.remote );
		Write( msg, v.isOnline );
	}
	public static void Read( Message msg, out List<NGRoomPlayerInfo> values )
	{
		values = new List<NGRoomPlayerInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGRoomPlayerInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGRoomPlayerInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGRoomInfo packet
	*/ 
	public static void Read( Message msg, out NGRoomInfo v )
	{
		v = new NGRoomInfo();
		Read( msg, out v.roomIndex );
		Read( msg, out v.isMatching );
		Read( msg, out v.roomState );
		Read( msg, out v.vecPlayerInfo );
		Read( msg, out v.hostAccountID );
	}
	public static void Write( Message msg, NGRoomInfo v )
	{
		Write( msg, v.roomIndex );
		Write( msg, v.isMatching );
		Write( msg, v.roomState );
		Write( msg, v.vecPlayerInfo );
		Write( msg, v.hostAccountID );
	}
	public static void Read( Message msg, out List<NGRoomInfo> values )
	{
		values = new List<NGRoomInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGRoomInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGRoomInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGPlayEndInfo packet
	*/ 
	public static void Read( Message msg, out NGPlayEndInfo v )
	{
		v = new NGPlayEndInfo();
		Read( msg, out v.playerIndex );
		Read( msg, out v.nick );
		Read( msg, out v.accountId );
		Read( msg, out v.rank );
		Read( msg, out v.round );
		Read( msg, out v.endTime );
		Read( msg, out v.attackDamage );
		Read( msg, out v.killCount );
		Read( msg, out v.gold );
		Read( msg, out v.epicTowerCount );
	}
	public static void Write( Message msg, NGPlayEndInfo v )
	{
		Write( msg, v.playerIndex );
		Write( msg, v.nick );
		Write( msg, v.accountId );
		Write( msg, v.rank );
		Write( msg, v.round );
		Write( msg, v.endTime );
		Write( msg, v.attackDamage );
		Write( msg, v.killCount );
		Write( msg, v.gold );
		Write( msg, v.epicTowerCount );
	}
	public static void Read( Message msg, out List<NGPlayEndInfo> values )
	{
		values = new List<NGPlayEndInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGPlayEndInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGPlayEndInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGCollectionInfo packet
	*/ 
	public static void Read( Message msg, out NGCollectionInfo v )
	{
		v = new NGCollectionInfo();
		Read( msg, out v.Type1 );
		Read( msg, out v.Type2 );
		Read( msg, out v.Type3 );
		Read( msg, out v.Value1 );
	}
	public static void Write( Message msg, NGCollectionInfo v )
	{
		Write( msg, v.Type1 );
		Write( msg, v.Type2 );
		Write( msg, v.Type3 );
		Write( msg, v.Value1 );
	}
	public static void Read( Message msg, out List<NGCollectionInfo> values )
	{
		values = new List<NGCollectionInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGCollectionInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGCollectionInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGChallengeModeEndInfo packet
	*/ 
	public static void Read( Message msg, out NGChallengeModeEndInfo v )
	{
		v = new NGChallengeModeEndInfo();
		Read( msg, out v.MapID );
		Read( msg, out v.ClearLife );
		Read( msg, out v.PlayTime );
		Read( msg, out v.EpicTowerCount );
		Read( msg, out v.Gold );
		Read( msg, out v.MissionClearCount );
		Read( msg, out v.GoldMonsterLevel );
		Read( msg, out v.KillMonsterCount );
		Read( msg, out v.KillBossMonsterCount );
		Read( msg, out v.TotalScore );
	}
	public static void Write( Message msg, NGChallengeModeEndInfo v )
	{
		Write( msg, v.MapID );
		Write( msg, v.ClearLife );
		Write( msg, v.PlayTime );
		Write( msg, v.EpicTowerCount );
		Write( msg, v.Gold );
		Write( msg, v.MissionClearCount );
		Write( msg, v.GoldMonsterLevel );
		Write( msg, v.KillMonsterCount );
		Write( msg, v.KillBossMonsterCount );
		Write( msg, v.TotalScore );
	}
	public static void Read( Message msg, out List<NGChallengeModeEndInfo> values )
	{
		values = new List<NGChallengeModeEndInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGChallengeModeEndInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGChallengeModeEndInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGChallengeRanker packet
	*/ 
	public static void Read( Message msg, out NGChallengeRanker v )
	{
		v = new NGChallengeRanker();
		Read( msg, out v.AccountID );
		Read( msg, out v.Nickname );
		Read( msg, out v.Rank );
		Read( msg, out v.BadgeIndex );
		Read( msg, out v.endInfo );
    }
	public static void Write( Message msg, NGChallengeRanker v )
	{
		Write( msg, v.AccountID );
		Write( msg, v.Nickname );
		Write( msg, v.Rank );
		Write( msg, v.BadgeIndex );
		Write( msg, v.endInfo );
    }
	public static void Read( Message msg, out List<NGChallengeRanker> values )
	{
		values = new List<NGChallengeRanker>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGChallengeRanker value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGChallengeRanker> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGSkillInfo packet
	*/ 
	public static void Read( Message msg, out NGSkillInfo v )
	{
		v = new NGSkillInfo();
		Read( msg, out v.skillInfo );
		Read( msg, out v.tmRegDate );
	}
	public static void Write( Message msg, NGSkillInfo v )
	{
		Write( msg, v.skillInfo );
		Write( msg, v.tmRegDate );
	}
	public static void Read( Message msg, out List<NGSkillInfo> values )
	{
		values = new List<NGSkillInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGSkillInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGSkillInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGProfileInfo packet
	*/ 
	public static void Read( Message msg, out NGProfileInfo v )
	{
		v = new NGProfileInfo();
		Read( msg, out v.iClearedLastStage );
		Read( msg, out v.iAcquiredEpicTower );
		Read( msg, out v.iClearedGeneralMission );
		Read( msg, out v.iClearedBossMission );
		Read( msg, out v.iAcquiredTrohpy );
		Read( msg, out v.iTotalPlayTime );
		Read( msg, out v.iMaximumDefeatedGoldMonsterLevel );
		Read( msg, out v.iAcquiredGold );
		Read( msg, out v.iDefeatedMonsterCount );
		Read( msg, out v.iDefeatedBossMonsterCount );
		Read( msg, out v.iBadgeIndex );
	}
	public static void Write( Message msg, NGProfileInfo v )
	{
		Write( msg, v.iClearedLastStage );
		Write( msg, v.iAcquiredEpicTower );
		Write( msg, v.iClearedGeneralMission );
		Write( msg, v.iClearedBossMission );
		Write( msg, v.iAcquiredTrohpy );
		Write( msg, v.iTotalPlayTime );
		Write( msg, v.iMaximumDefeatedGoldMonsterLevel );
		Write( msg, v.iAcquiredGold );
		Write( msg, v.iDefeatedMonsterCount );
		Write( msg, v.iDefeatedBossMonsterCount );
		Write( msg, v.iBadgeIndex );
	}
	public static void Read( Message msg, out List<NGProfileInfo> values )
	{
		values = new List<NGProfileInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGProfileInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGProfileInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGEndlessModeEndInfo packet
	*/ 
	public static void Read( Message msg, out NGEndlessModeEndInfo v )
	{
		v = new NGEndlessModeEndInfo();
		Read( msg, out v.iMaxRound );
		Read( msg, out v.i64PlayTime );
		Read( msg, out v.iEpicTowerCount );
		Read( msg, out v.iGold );
		Read( msg, out v.iMissionClearCount );
		Read( msg, out v.iGoldMonsterLevel );
		Read( msg, out v.iKillMonsterCount );
		Read( msg, out v.iKillBossMonsterCount );
		Read( msg, out v.iUsedTechPoint );
		Read( msg, out v.iTotalScore );
	}
	public static void Write( Message msg, NGEndlessModeEndInfo v )
	{
		Write( msg, v.iMaxRound );
		Write( msg, v.i64PlayTime );
		Write( msg, v.iEpicTowerCount );
		Write( msg, v.iGold );
		Write( msg, v.iMissionClearCount );
		Write( msg, v.iGoldMonsterLevel );
		Write( msg, v.iKillMonsterCount );
		Write( msg, v.iKillBossMonsterCount );
		Write( msg, v.iUsedTechPoint );
		Write( msg, v.iTotalScore );
	}
	public static void Read( Message msg, out List<NGEndlessModeEndInfo> values )
	{
		values = new List<NGEndlessModeEndInfo>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGEndlessModeEndInfo value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGEndlessModeEndInfo> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGEndlessModeRanker packet
	*/ 
	public static void Read( Message msg, out NGEndlessModeRanker v )
	{
		v = new NGEndlessModeRanker();
		Read( msg, out v.AccountID );
		Read( msg, out v.Nickname );
		Read( msg, out v.Rank );
		Read( msg, out v.BadgeIndex );
		Read( msg, out v.endInfo );
    }
	public static void Write( Message msg, NGEndlessModeRanker v )
	{
		Write( msg, v.AccountID );
		Write( msg, v.Nickname );
		Write( msg, v.Rank );
		Write( msg, v.BadgeIndex );
		Write( msg, v.endInfo );
    }
	public static void Read( Message msg, out List<NGEndlessModeRanker> values )
	{
		values = new List<NGEndlessModeRanker>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGEndlessModeRanker value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGEndlessModeRanker> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
	/* 
	NGLogInAck packet
	*/ 
	public static void Read( Message msg, out NGLogInAck v )
	{
		v = new NGLogInAck();
		Read( msg, out v.userInfo );
		Read( msg, out v.vecCollectionInfo );
		Read( msg, out v.vecChallengeModeEndInfo );
		Read( msg, out v.vecSkillInfo );
		Read( msg, out v.profileInfo );
		Read( msg, out v.endlessModeEndInfo );
	}
	public static void Write( Message msg, NGLogInAck v )
	{
		Write( msg, v.userInfo );
		Write( msg, v.vecCollectionInfo );
		Write( msg, v.vecChallengeModeEndInfo );
		Write( msg, v.vecSkillInfo );
		Write( msg, v.profileInfo );
		Write( msg, v.endlessModeEndInfo );
	}
	public static void Read( Message msg, out List<NGLogInAck> values )
	{
		values = new List<NGLogInAck>();
		Int16 count = 0;
		msg.Read( out count );
		for( int i = 0; i < count; ++i )
		{
			NGLogInAck value2;
			Read( msg, out value2 );
			values.Add( value2 );
		}
	}
	public static void Write( Message msg, List<NGLogInAck> values )
	{
		if( values == null )
		{
			msg.Write( (Int16)0 );
			return;
		}
		msg.Write( (Int16)values.Count );
		for( int i = 0; i < values.Count; ++i )
		{
			Write( msg, values[i] );
		}
	}
}
