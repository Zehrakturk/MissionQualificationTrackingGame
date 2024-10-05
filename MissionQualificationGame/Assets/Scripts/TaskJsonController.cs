
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskJsonController : MonoBehaviour
{
    public int taskID;
    public TextMeshProUGUI taskStatusText;
    public TextMeshProUGUI taskSkillText;
    public TextMeshProUGUI taskDescriptionText;
    public TextMeshProUGUI taskOptionsText;
    public TextMeshProUGUI taskInputText;
    public Button saveButton;
    public Button loadButton;

    private ITaskDataManager taskManager;
    public TaskStatsController currentTask;
    GizliGorev gizliGorev;

    void Start()
    {
        gizliGorev = FindObjectOfType<GizliGorev>();

        taskSkillText = GameObject.Find("Skills").GetComponent<TextMeshProUGUI>();
        taskDescriptionText = GameObject.Find("descriptions").GetComponent<TextMeshProUGUI>();
        taskOptionsText = GameObject.Find("options").GetComponent<TextMeshProUGUI>();
        taskInputText = GameObject.Find("input").GetComponent<TextMeshProUGUI>();
        taskStatusText = GameObject.Find("status").GetComponent<TextMeshProUGUI>();

        saveButton = GameObject.Find("Save").GetComponent<Button>();
        loadButton = GameObject.Find("Load").GetComponent<Button>();

        taskManager = new TaskManager();

        if (currentTask != null)
        {
            UpdateUI(currentTask);
        }
        else
        {
            Debug.Log("Kayýtlý görev bulunamadý. Yeni görev oluþturuluyor.");
        }

        saveButton.onClick.AddListener(SaveTask);
        loadButton.onClick.AddListener(LoadTask);
    }

    void Update()
    {
        SaveTask();
    }

    public void SaveTask()
    {
        TaskStatsController.stations currentStation = TaskStatsController.stations.Tamamlandı;

        // Skill listesi için tüm TextMeshProUGUI child'larýný alýyoruz
        TextMeshProUGUI[] childSkillTexts = taskSkillText.GetComponentsInChildren<TextMeshProUGUI>();

        List<TaskStatsController.skills> skillList = new List<TaskStatsController.skills>();

        // Her bir child TextMeshProUGUI bileþeni üzerinde dönüyoruz
        foreach (var childSkillText in childSkillTexts)
        {
            string skillString = childSkillText.text.Trim();

            if (Enum.TryParse(skillString, out TaskStatsController.skills parsedSkill))
            {
                skillList.Add(parsedSkill);
            }

        }

        currentTask = new TaskStatsController(
            taskID,
            currentStation,
            skillList,  // Skill listesi artýk birden fazla skill'i içeriyor
            taskDescriptionText.text,
            taskOptionsText.text.Split(',').ToList(),
            taskInputText.text
        );

        taskManager.Save(currentTask);
        //Debug.Log("Görev kaydedildi.");
    }


    public void LoadTask()
    {
        currentTask = taskManager.Load();

        if (currentTask != null)
        {
            UpdateUI(currentTask);
            Debug.Log("Görev yüklendi.");
        }
        else
        {
            Debug.Log("Kayýtlý görev bulunamadý.");
        }
    }

    private void UpdateUI(TaskStatsController task)
    {
        taskID = task.taskID;
        taskStatusText.text = task.station.ToString();
        taskSkillText.text = string.Join(",", task.skillList.Select(skill => skill.ToString())); // Skill listesi olarak güncelleme
        taskDescriptionText.text = task.descriptions;
        taskOptionsText.text = task.options != null ? string.Join(",", task.options) : "";
        taskInputText.text = task.input;
    }
}
