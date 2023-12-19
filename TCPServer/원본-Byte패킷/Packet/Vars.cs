using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NGEL;

[Serializable]
public class NGPairInt2
{
    public int first = 0;
    public int second = 0;

    public NGPairInt2()
    {
    }
    public NGPairInt2(int _first, int _second)
    {
        first = _first;
        second = _second;
    }
}

[Serializable]
public class Vec2
{
    public float x = 0;
    public float y = 0;

    public Vec2()
    {
    }
    public Vec2(float _x, float _y)
    {
        x = _x;
        y = _y;
    }
}

public class FarmClientInfo
{
    public int serverMode = 0;
    public int gamerCount = 0;
    public string HostName;
    public int Port;
    public int ServerGroupID = 1;
}

public class NGServerGroupInfo
{
    public int GroupID = 1;
    public string CenterServerIp;
    public int CenterServerPort = -1;
    public string ServerName;
};

public class NGUserInfo
{
    public string NickName;
    public string Nation;
    public DateTime RegDate;
    public DateTime LastLoginDate;
    public Int64 AccountID = -1;
}

public class NGAccountInfo
{
    public string DeviceID;
    public Int64 AccountID = 0;
    public int AuthType = 0;
    public int ServerGroup = 1;
    public DateTime RegDate;
    public DateTime LastLogInDate;
}

//비밀미션 조건
public class SecretMissionConditionSync
{
    public int NameID;
    public int DescID;
    public int type;
    public int score;
    public int ratio;
    public int tower;
    public int count;
    public int condition;

    public SecretMissionConditionSync()
    {
    }
}

//비밀미션 보상
public class SecretMissionRewardScriptInfo
{
    public int score;
    public int ratio;
    public int reward;
    public int count;
}

//비밀미션
public class SecretMissionSync
{
    public SecretMissionConditionSync missionCondition;
    public SecretMissionRewardScriptInfo rewardInfo;
    public bool clearMission = false;
    public int progress = 0;
    public SecretMissionSync()
    {

    }
}

[Serializable]
public class NGMonsterObjectSync
{
    public Int64 uid; //아이디
    public int monsterID; //몬스터타입
    public int lifeStat; //속성
    public int pathIndex; //몇번째 길인지?
    public Vec2 pos; //위치
    public int hp; //체력

    public NGMonsterObjectSync()
    {

    }
}

/// <summary>
/// 각각의 타워가 가진 값들(서버에 넘기기용)
/// </summary>
[Serializable]
public class NGTowerObjectSync
{
    public Int64 uid; //유저아이디?
    public int towerID; //타워종류
    public int lifeStat; //체력타입?
    public int areaIndex; //타워위치
    public int killCount; //처치 수
    public float attackDelay; //공격 딜레이
    public float skillCoolDown; //스킬 쿨타임
    public bool useOnceSkill; //1회용스킬 사용여부

    public NGTowerObjectSync()
    {

    }
}

public class NGProjectileObjectSync
{
    public Int64 uid;
    public int projectileID; //투사체종류
    public int lifeStat; //공격타입?
    public Vec2 pos; //위치
    public bool attackEffect; //이펙트
    public int damage; //데미지
    public int upgradeDamage; //강화데미지
    public Int64 targetMonster; //도착지인 몬스터
    public Int64 parentTower; //출발지인 타워

    public NGProjectileObjectSync()
    {

    }
}

[Serializable]
public class NGBaseFrameSync
{
    public int NowTurn; //현재 웨이브
    public int playerState; //플레이어상태
    public int lifeCount; //남은 HP
    public int money; // 돈
    public int rareTicket; // 레어타워 소환티켓
    public int uniqueTicket; // 유니크타워 소환티켓
    public int epicTicket; // 에픽타워 소환티켓
    public int monsterCount; //남은 몬스터 수
    public int maxMonsterCount; //이번 웨이브 몬스터 수
    public int towerScore; //타워점수(보유한 공격점수)
    public int attackMonster1Count; //첫번째 공격몬스터
    public int attackMonster2Count; //두번째 공격몬스터
    public int attackMonster3Count; //세번째 공격몬스터
    public int userAttackPoint; //공격을 성공한 수
    public int userKillPoint;//적을 죽인 수
    public int useTowerScore;
    public int gold;
    public int epicTowerCount;
    public List<int> missionCounts;

    public NGBaseFrameSync()
    {

    }
    public NGBaseFrameSync(int life, int _money)
    {
        lifeCount = life;
        money = _money;

        NowTurn = 0;
        playerState = 0;
        rareTicket = 0;
        uniqueTicket = 0;
        epicTicket = 0;
        monsterCount = 0;
        maxMonsterCount = 0;
        towerScore = 0;
        attackMonster1Count = 0;
        attackMonster2Count = 0;
        attackMonster3Count = 0;
        userAttackPoint = 0;
        userKillPoint = 0;
        useTowerScore = 0;
        gold = 0;
        epicTowerCount = 0;
        missionCounts = new List<int>();

        for (int i = 0; i < 9; ++i)
        {
            missionCounts.Add(0);
        }
    }
}

[Serializable]
public class NGFrameSync : NGBaseFrameSync
{
    public Int64 AccountID;
    public List<NGPairInt2> upgradeCount; //key: upgradeType, value: upgradeCount
    public int personalMissionCount; // 개인미션 진행도
    public List<SecretMissionSync> vecSecretMissionInfo; //비밀미션 정보
    public List<NGMonsterObjectSync> vecMonsterSync; //몬스터정보
    public List<NGTowerObjectSync> vecTowerSync; //타워정보
    public List<NGProjectileObjectSync> vecProjectileSync; //투사체정보

    public NGFrameSync()
    {
        upgradeCount = new List<NGPairInt2>();
        int length = 3;
        for (int i = 0; i < length; i++)
        {
            upgradeCount.Add(new NGPairInt2());
            upgradeCount[i].first = i;
        }

        personalMissionCount = 0;
        vecSecretMissionInfo = new List<SecretMissionSync>();
        vecMonsterSync = new List<NGMonsterObjectSync>();
        vecTowerSync = new List<NGTowerObjectSync>();
        vecProjectileSync = new List<NGProjectileObjectSync>();
    }

    public void ClearData()
    {
        upgradeCount = null;
        vecSecretMissionInfo = null;
        vecMonsterSync = null;
        vecTowerSync = null;
        vecProjectileSync = null;
    }
}

public class NGVRBaseFrameSync
{
    public int NowTurn; //현재 웨이브
    public int playerState; //플레이어상태
    public int lifeCount; //남은 HP
    public int money; // 돈
    public int rareTicket; // 레어타워 소환티켓
    public int uniqueTicket; // 유니크타워 소환티켓
    public int epicTicket; // 에픽타워 소환티켓
    public int monsterCount; //남은 몬스터 수
    public int maxMonsterCount; //이번 웨이브 몬스터 수
    public int towerScore; //타워점수(보유한 공격점수)
    public int attackMonster1Count; //첫번째 공격몬스터 _추가
    public int attackMonster2Count; //두번째 공격몬스터 _추가
    public int attackMonster3Count; //세번째 공격몬스터 _추가
    public int userAttackPoint; //공격을 성공한 수
    public int userKillPoint;//적을 죽인 수

    public int firstMissionCount; // 첫번째 미션 카운트
    public int secondMissionCount; // 두번째 미션 카운트
    public int thirdMissionCount; // 세번째 미션 카운트
    public int FourthMissionCount; // 네번째 미션 카운트
    public int FifthMissionCount; // 다섯번째 미션 카운트
    public int SixthMissionCount; // 여섯번째 미션 카운트
    public int SeventhMissionCount; // 일곱번째 미션 카운트
    public int EighthMissionCount; // 여덟번째 미션 카운트
    public int NinethMissionCount; // 아홉번째 미션 카운트

    public NGVRBaseFrameSync()
    {

    }
    public NGVRBaseFrameSync(int life, int _money)
    {
        lifeCount = life;
        money = _money;

        NowTurn = 0;
        playerState = 0;
        rareTicket = 0;
        uniqueTicket = 0;
        epicTicket = 0;
        monsterCount = 0;
        maxMonsterCount = 0;
        towerScore = 0;
        attackMonster1Count = 0;
        attackMonster2Count = 0;
        attackMonster3Count = 0;
        userAttackPoint = 0;
        userKillPoint = 0;
    }
}

[Serializable]
public class NGVRFrameSync : NGVRBaseFrameSync
{
    public List<NGPairInt2> upgradeCount; //key: upgradeType, value: upgradeCount
    public int personalMissionCount; // 개인미션 진행도
    public List<SecretMissionSync> vecSecretMissionInfo; //비밀미션 정보
    public List<NGMonsterObjectSync> vecMonsterSync; //몬스터정보
    public List<NGTowerObjectSync> vecTowerSync; //타워정보
    public List<NGProjectileObjectSync> vecProjectileSync; //투사체정보

    public NGVRFrameSync()
    {
        upgradeCount = new List<NGPairInt2>();
        int length = 3;
        for (int i = 0; i < length; i++)
        {
            upgradeCount.Add(new NGPairInt2());
            upgradeCount[i].first = i;
        }

        personalMissionCount = 0;
        vecSecretMissionInfo = new List<SecretMissionSync>();
        vecMonsterSync = new List<NGMonsterObjectSync>();
        vecTowerSync = new List<NGTowerObjectSync>();
        vecProjectileSync = new List<NGProjectileObjectSync>();
    }

    public void ClearData()
    {
        upgradeCount = null;
        vecSecretMissionInfo = null;
        vecMonsterSync = null;
        vecTowerSync = null;
        vecProjectileSync = null;
    }
}

public enum EROOMPLAYER_STATE
{
    EMPTY = 0,
    INVITE = 1, //초대중
    WAIT = 2,//방 참가 후 대기 중
    READY = 3,//준비됨
    LEAVE = 4,
}

[Serializable]
public class NGRoomPlayerInfo
{
    public Int64 AccountID = -1;
    public int playerState = 0;
    public int Index = 0;  // 들어온 순서
    public string nick;
    public Int64 remote = -1;	// 서버에서만 사용됨(서버 사용자 식별키)
    public bool isOnline = false;

    //public bool isChoiced = false; // 공격할때 겹치지 않도록_패킷 안받음
}

public enum EROOMSTATE
{
    INIT = 0,//최초 방생성 시
    PLAY_GAME,//방장이 게임 시작
    END,
}

[Serializable]
public class NGRoomInfo
{
    public Int64 roomIndex = -1; //룸 번호
    public bool isMatching = false; //매칭 여부
    public int roomState = 0; //현재 룸의 상태( EROOMSTATE의 enum을 따른다 )
    public List<NGRoomPlayerInfo> vecPlayerInfo;//방에 접속중인 유저 정보
    public Int64 hostAccountID = -1; //호스트
}

[Serializable]
public class NGPlayEndInfo
{
    public int playerIndex = 0;
    public string nick;
    public Int64 accountId;
    public int rank;
    public int round = 0;
    public Int64 endTime;
    public int attackDamage;
    public int killCount;
    public int gold = 0;
    public int epicTowerCount = 0;
}

public class NGLogInAck
{
    public NGUserInfo userInfo;
    public List<NGCollectionInfo> vecCollectionInfo;
    public List<NGChallengeModeEndInfo> vecChallengeModeEndInfo;
    public List<NGSkillInfo> vecSkillInfo;
    public NGProfileInfo profileInfo;
    public NGEndlessModeEndInfo endlessModeEndInfo;
}

public class NGCollectionInfo
{
    public Int64 Type1 = -1;
    public Int64 Type2 = -1;
    public Int64 Type3 = -1;
    public Int64 Value1 = 0;
}

[Serializable]
public class NGChallengeModeEndInfo
{
    public int MapID = -1;
    public int ClearLife = 0;
    public Int64 PlayTime = 0;
    public int EpicTowerCount = 0;
    public int Gold = 0;
    public int MissionClearCount = 0;
    public int GoldMonsterLevel = 0;
    public int KillMonsterCount = 0;
    public int KillBossMonsterCount = 0;
    public int TotalScore = 0;
}

[Serializable]
public class NGEndlessModeEndInfo
{
    public int iMaxRound = 0;
    public Int64 i64PlayTime = 0;
    public int iEpicTowerCount = 0;
    public int iGold = 0;
    public int iMissionClearCount = 0;
    public int iGoldMonsterLevel = 0;
    public int iKillMonsterCount = 0;
    public int iKillBossMonsterCount = 0;
    public int iUsedTechPoint = 0;
    public int iTotalScore = 0;
}

[Serializable]
public class NGChallengeRanker
{
    public Int64 AccountID;
    public string Nickname;
    public int Rank;
    public int BadgeIndex;
    public NGChallengeModeEndInfo endInfo;
}

[Serializable]
public class NGEndlessModeRanker
{
    public Int64 AccountID;
    public string Nickname;
    public int Rank;
    public int BadgeIndex;
    public NGEndlessModeEndInfo endInfo;
}

[Serializable]
public class PlayerSkillTree
{
    public int totalPoint;
    public int leftPoint;
    public List<SkillTree> skillTrees;

    public PlayerSkillTree()
    {
        totalPoint = 0;
        leftPoint = 0;
        skillTrees = new List<SkillTree>();

        int skillCount = Enum.GetValues(typeof(PlayerSkillType)).Length;
        for (int i = 0; i < skillCount; i++)
        {
            SkillTree tmp = new SkillTree((PlayerSkillType)i);
            skillTrees.Add(tmp);
        }
    }
}

[Serializable]
public class SkillTree
{
    public PlayerSkillType playerSkillType;
    public float playerSkillvalue;
    public int unlockPoint;

    public SkillTree(PlayerSkillType _type)
    {
        playerSkillType = _type;
        playerSkillvalue = 0;
        unlockPoint = 0;
    }
}

public enum PlayerSkillType
{
    GetRareTower,
    GetUniqueTower,
    WaitingTime,
    MaxHP,
    BossDamage,
    BossHP,
    BossDefense,
    BossSpeed,
    MonsterHP,
    MonsterDefense,
    MonsterResistance,
    MonsterSpeed,
    BossMoney,
    RoundMoney,
    GoldMonsterHP,
    GoldMonsterCoolTime,
    GoldMonsterSpeed,
    GoldMonsterMoney,
    UpgradeExis,
    UpgradeUnion,
    UpgradeDemic,
    None
}

public enum SkillState
{
    Lean,
    Clickable,
    Off,
    None
}

public class NGSkillInfo
{
    public NGPairInt2 skillInfo;
    public DateTime tmRegDate;
}

[Serializable]
public class NGProfileInfo
{
    public int iClearedLastStage; // 클리어 스테이지 ( 0 ~ 19 )
    public int iAcquiredEpicTower; // 획득 에픽타워 수
    public int iClearedGeneralMission; // 일반 미션 완료 횟수
    public int iClearedBossMission; // 보스 미션 완료 횟수
    public int iAcquiredTrohpy; // 달성 트로피 수
    public int iTotalPlayTime; // 총 플레이 시간
    public int iMaximumDefeatedGoldMonsterLevel; // 골드 몬스터 최대 레벨
    public int iAcquiredGold; // 획득 골드
    public int iDefeatedMonsterCount; // 처치한 몬스터 수
    public int iDefeatedBossMonsterCount; // 처치한 보스 몬스터 수
    public int iBadgeIndex; // 선택한 뱃지 번호
}