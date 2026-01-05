using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOnDie : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] CharacterHealth playerHealth;

    void Awake()
    {
        playerHealth.onDeath.AddListener(OnDeath);
    }

    void OnDeath(Health arg0)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
