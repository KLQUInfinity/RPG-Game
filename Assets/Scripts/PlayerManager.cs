using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    #region Singleton

    public static PlayerManager Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    public GameObject Player;

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
