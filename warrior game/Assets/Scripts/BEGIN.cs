﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class BEGIN : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
