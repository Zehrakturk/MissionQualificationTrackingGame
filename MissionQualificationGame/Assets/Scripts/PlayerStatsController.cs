using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerStatsController
{
    public string level;
    public string xpBar;
    public string ypBar;
    public string software;
    public string management;
    public float posX;
    public float posY;
    public float posZ;
    public int sceneID;

    // Parameterless constructor
    public PlayerStatsController() { }

    public PlayerStatsController(string level, string xpBar, string ypBar, string software, string management,
        float posX, float posY, float posZ, int sceneID)
    {
        this.level = level;
        this.xpBar = xpBar;
        this.ypBar = ypBar;
        this.software = software;
        this.management = management;
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        this.sceneID = sceneID;
    }
}

public interface IPlayerDataManager
{
    void Save(PlayerStatsController playerStats);
    PlayerStatsController Load();
}

public interface IUIUpdater
{
    void UpdateUI(PlayerStatsController playerStats);
    void ShowWarning(string message);
    void HideWarning();
}

public class PlayerDataManager : IPlayerDataManager
{
    private string filePath = Application.dataPath + "/Saves/userJson.json";

    public void Save(PlayerStatsController playerStats)
    {
        string jsonString = JsonUtility.ToJson(playerStats);
        File.WriteAllText(filePath, jsonString);
    }

    public PlayerStatsController Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerStatsController>(json);
        }
        return null;
    }
}
