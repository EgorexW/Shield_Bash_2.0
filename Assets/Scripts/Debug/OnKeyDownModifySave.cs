using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class OnKeyDownModifySave : OnKeyDownTrigger
{
    [SerializeField] ModifySaveEffect modifySaveEffect;

    protected override void Trigger()
    {
        // AI GENERATED

        var path = SaveSystem.GetPath(1);

        if (!File.Exists(path)){
            Debug.LogWarning("Save file not found: " + path);
            return;
        }

        switch (modifySaveEffect){
            case ModifySaveEffect.Open:
                try{
                    if (Application.platform == RuntimePlatform.WindowsPlayer ||
                        Application.platform == RuntimePlatform.WindowsEditor){
                        Process.Start(new ProcessStartInfo(path){ UseShellExecute = true });
                    }
                    else if (Application.platform == RuntimePlatform.OSXPlayer ||
                             Application.platform == RuntimePlatform.OSXEditor){
                        Process.Start("open", "\"" + path + "\""); // Wrap in quotes to handle spaces
                    }
                    else if (Application.platform == RuntimePlatform.LinuxPlayer ||
                             Application.platform == RuntimePlatform.LinuxEditor){
                        Process.Start("xdg-open", "\"" + path + "\""); // Wrap in quotes
                    }
                }
                catch (Exception e){
                    Debug.LogError("Failed to open log: " + e);
                }
                break;
            case ModifySaveEffect.Delete:
                try{
                    File.Delete(path);
                    Debug.Log("Deleted save file: " + path);
                }
                catch (Exception e){
                    Debug.LogError("Failed to delete save file: " + e);
                }
                break;
        }
    }
}

enum ModifySaveEffect
{
    Open,
    Delete
}