using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        // Karakterin konumunu geri yükle
        float x = PlayerPrefs.GetFloat("PlayerX", 0);
        float y = PlayerPrefs.GetFloat("PlayerY", 0);
        float z = PlayerPrefs.GetFloat("PlayerZ", 0);
        Vector3 spawnPosition = new Vector3(x, y, z);


        // Karakteri belirtilen konumda spawnla
        // Yalnýzca prefab instantiate edilmiyorsa oyuncuyu oluþtur

    }
}
