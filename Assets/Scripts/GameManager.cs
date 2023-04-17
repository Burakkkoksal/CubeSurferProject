using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SwerveInputSystem SwerveInputSystem;
    public SwerveMovement SwerveMovement;
    public PlayerMoverRunner PlayerMoverRunner;




    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }
    #endregion
}
