using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableData/SpriteDB", fileName = "SpriteDB")]
public class SpriteDB : SerializedScriptableObject
{
    //[SerializeField]
    //private Dictionary<string, Sprite> spriteDict;

    [SerializeField]
    private List<SpriteData> spriteDatas;

    //public Sprite GetSprite(string key) => spriteDict.ContainsKey(key) ? spriteDict[key] : null;

    public Sprite GetSprite(string key)
    {
        var data = spriteDatas.Find(s => s.ID == key);
        if (data == null)
            return null;

        return data.MySprite;
    }


    [Serializable]
    public class SpriteData
    {
        [SerializeField]
        private string id;
        [PreviewField]
        [SerializeField]
        private Sprite mySprite;

        #region 프로퍼티
        public string ID { get => id; }
        public Sprite MySprite { get => mySprite; }
        #endregion

        public SpriteData(string id, Sprite spr)
        {
            this.id = id;
            mySprite = spr;
        }
    }
}
