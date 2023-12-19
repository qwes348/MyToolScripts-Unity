using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMUserInfo : Singleton2<NMUserInfo>
{
    internal PlayerSkillTree playerSkillTree = new PlayerSkillTree();
    internal NGLogInAck loginInfo;
    internal NGAccountInfo AccountInfo;

    internal List<NGSkillInfo> currentSkillInfo;
    internal List<NGCollectionInfo> currentCollectionInfo;
    internal NGProfileInfo currentProfileInfo;

    internal Int64 GetAccountId()
    {
        if (AccountInfo == null) return 0;
        return AccountInfo.AccountID;
    }

    internal string GetNick()
    {
        if (loginInfo == null) return "";
        return loginInfo.userInfo.NickName;
    }

    internal NGLogInAck GetInfo()
    {
        if (loginInfo == null) return null;
        return loginInfo;
    }

}
