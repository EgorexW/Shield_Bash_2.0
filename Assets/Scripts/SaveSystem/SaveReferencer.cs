using UnityEngine;

public abstract class SaveReferencer : MonoBehaviour
{
    public abstract void OnSave(SaveData data);
    public abstract void OnLoad(SaveData data);
}