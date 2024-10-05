using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBarController : MonoBehaviour
{
    private static LevelBarController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sahne ID'si 2 veya 3 deðilse bu GameObject'i yok et
        if (scene.buildIndex != 2 && scene.buildIndex != 3)
        {
            Destroy(gameObject);
        }
    }
}
