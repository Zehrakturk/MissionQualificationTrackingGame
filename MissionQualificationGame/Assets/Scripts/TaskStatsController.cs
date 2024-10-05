using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class TaskStatsController
{
    public enum stations
    {
        DevamEdiyor,
        Beklemede,
        Tamamlandý
    }

    public enum skills
    {
        Software,
        Managemenet,
        Hardware
        
    }

    public int taskID;
    public stations station;
    public List<skills> skillList;  // Tek skill yerine bir liste
    public List<string> options;
    public string descriptions;
    public string input;

    // Constructor
    public TaskStatsController(int taskID, stations station, List<skills> skillList, string description,
        List<string> options = null, string input = null)
    {
        this.taskID = taskID;
        this.station = station;
        this.skillList = skillList; // Tek skill yerine skill listesi
        this.descriptions = description;
        this.options = options;
        this.input = input;
    }
}


public interface ITaskDataManager
{
    void Save(TaskStatsController playerStats);
    TaskStatsController Load();
}

public class TaskManager : ITaskDataManager
{
    private string filePath = Application.dataPath + "/Saves/taskJson.json";

    public void Save(TaskStatsController task)
    {
        string jsonString = JsonUtility.ToJson(task);
        File.WriteAllText(filePath, jsonString);
    }

    public TaskStatsController Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<TaskStatsController>(json);
        }
        return null;
    }
}
