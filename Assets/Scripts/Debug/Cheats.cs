using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PlayerOnDie playerOnDie;
    
    public bool dontDie = false;

    void Awake()
    {
        if (dontDie){
            Debug.Log("Cheat Active: Player cannot die");
            Destroy(playerOnDie);
        }
    }
}
