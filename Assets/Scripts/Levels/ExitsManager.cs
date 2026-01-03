using System;
using UnityEngine;

public class ExitsManager : MonoBehaviour
    {
        LevelExit[] exits;

        void Awake()
        {
            exits = GetComponentsInChildren<LevelExit>();
        }

        public void SetBlock(bool block)
        {
            foreach (LevelExit exit in exits){
                exit.SetBlock(block);
            }
        }
    }