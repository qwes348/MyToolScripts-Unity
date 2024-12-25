using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager
{
    [SerializeField]
    public SaveData localSaveData;
    
    private string SavePath => Path.Combine(Application.persistentDataPath, "Save.json");
    
    public void Init()
    {
        Load();
    }

    public void Save()
    {
        if(localSaveData == null)
            localSaveData = new SaveData();
        
        JObject jobj = JObject.FromObject(localSaveData);
        
        string json = JsonConvert.SerializeObject(jobj, Formatting.Indented);
        File.WriteAllText(SavePath, json);
    }

    public void Load()
    {
        if (!File.Exists(SavePath))
        {
            Save();
            return;
        }
        
        JObject jobj = JObject.Parse(File.ReadAllText(SavePath));
        localSaveData = jobj.ToObject<SaveData>();
    }
    
    [Serializable]
    public class SaveData
    {
        private int highScore = 0;

        #region 프로퍼티
        public int HighScore
        {
            get => highScore;
            set
            {
                highScore = value;
                Managers.SaveLoad.Save();
            }
        }
        #endregion
    }
}
