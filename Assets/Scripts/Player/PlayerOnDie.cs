using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOnDie : MonoBehaviour
{
    void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
