using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class acilirGorevController : MonoBehaviour
{
    public string balon;
    public string description1;
    public string description2;

    public acilirGorevController()
    {
        
    }
    public acilirGorevController(string balon, string description1, string description2)
    {
        this.balon = balon;
        this.description1 = description1;
        this.description2 = description2;
        
    }




}

public interface IGorevDataManager
{
    void Save(PlayerStatsController playerStats);
    PlayerStatsController Load();
}

public class GorevDataManager : IPlayerDataManager
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
