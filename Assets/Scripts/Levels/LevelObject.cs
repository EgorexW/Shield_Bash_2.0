using UnityEngine;

public class LevelObject : MonoBehaviour, ILevelObject
{
    public Level parentLevel{ get; set; }
}

public interface ILevelObject
{
    public Level parentLevel{ set; }
}