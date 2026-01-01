using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] DefaultSaveData defaultSaveData;
    
    public SaveData data;
    public List<SaveReferencer> saveReferencers = new();

    [SerializeField] int saveIndex = 1;
    
    public void Save()
    {
        foreach (SaveReferencer saveReference in saveReferencers){
            saveReference.OnSave(data);
        }
        File.WriteAllText(GetPath(saveIndex), JsonUtility.ToJson(data));
    }

    public void Load()
    {
        if (File.Exists(GetPath(saveIndex))){
            data = JsonUtility.FromJson<SaveData>(File.ReadAllText(GetPath(saveIndex)));
        }
        else{
            data = defaultSaveData.CreateDefaultSaveData();
        }
        foreach (SaveReferencer saveReference in saveReferencers){
            saveReference.OnLoad(data);
        }
    }

    public static string GetPath(int saveIndex)
    {
        return Application.persistentDataPath + $"/Shield Bash 2.0 save 0{saveIndex}.json";
    }
}