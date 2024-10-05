using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerJsonController : MonoBehaviour
{
    // Public fields for dragging and dropping references in the Unity Editor
    public TextMeshProUGUI xpBarText;
    public TextMeshProUGUI ypBarText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI softwareText;
    public TextMeshProUGUI managementText;
    public TextMeshProUGUI warningText;
    //public Button confirmButton;  
    public Button saveButton;
    public Button loadButton;
    public GameObject player;

    private IPlayerDataManager dataManager;
    private IUIUpdater uiUpdater;
    private PlayerStatsController playerStatsController;
    private LevelToggleController levelToggleController;
    GizliGorev gizliGorev;

    void Start()
    {
        // Check if any public references are missing
        CheckUIElements();

        dataManager = new PlayerDataManager();
        uiUpdater = new UIUpdater(xpBarText, ypBarText, levelText, softwareText, managementText, warningText);
        gizliGorev=FindAnyObjectByType<GizliGorev>();
        levelToggleController=FindObjectOfType<LevelToggleController>();



        // Initialize playerStatsController with current player position and scene ID
        playerStatsController = new PlayerStatsController
        {
            posX = player.transform.position.x,
            posY = player.transform.position.y,
            posZ = player.transform.position.z,
            sceneID = SceneManager.GetActiveScene().buildIndex
        };

        // Try to load data and show warning if it fails
        var loadedStats = dataManager.Load();
        if (loadedStats != null)
        {
            playerStatsController = loadedStats;
            Load();
        }
        else
        {
            uiUpdater.ShowWarning("Kayýtlý veri yüklenemedi. Yeni oyun baþlatýlýyor.");
            StartCoroutine(HideWarningTextAfterDelay(5f));
        }

        gizliGorev.confirmButton?.onClick.AddListener(Save);
        saveButton?.onClick.AddListener(Save);
        loadButton?.onClick.AddListener(() =>
        {
            Load();
            uiUpdater.HideWarning();
        });
    }

    public void Load()
    {
        if (playerStatsController != null)
        {
            player.transform.position = new Vector3(playerStatsController.posX, playerStatsController.posY, playerStatsController.posZ);
            StartCoroutine(LoadSceneAndSetPosition(playerStatsController.sceneID, player.transform.position));
            uiUpdater.UpdateUI(playerStatsController);

            if (float.TryParse(xpBarText.text.Replace("%", ""), out float xpValue))
            {
                // Fill amount'u hesapla ve level bar'a ata
                float fillAmount = xpValue / 100f;
                levelToggleController.levelBar.fillAmount = fillAmount;
                levelToggleController.UpdatePercentageText(); // Text'i güncelle
            }
            else
            {
                Debug.LogError("Failed to parse XP bar text to float.");
            }
        }
        else
        {
            Debug.LogError("playerStatsController is null in Load.");
        }
    }


    public void Save()
    {
        if (playerStatsController != null)
        {
            playerStatsController = new PlayerStatsController(
                levelText?.text ?? "N/A",
                xpBarText?.text ?? "N/A",
                ypBarText?.text ?? "N/A",
                softwareText?.text ?? "N/A",
                managementText?.text ?? "N/A",
                player.transform.position.x,
                player.transform.position.y,
                player.transform.position.z,
                SceneManager.GetActiveScene().buildIndex
            );
            uiUpdater.UpdateUI(playerStatsController);
            dataManager.Save(playerStatsController);
        }
        else
        {
            Debug.LogError("playerStatsController is null in Save.");
        }
    }

    IEnumerator HideWarningTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        uiUpdater.HideWarning();
    }

    private IEnumerator LoadSceneAndSetPosition(int sceneID, Vector3 position)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (player != null)
        {
            player.transform.position = position;
        }
        else
        {
            Debug.LogError("player is null after scene load.");
        }

        uiUpdater.UpdateUI(playerStatsController);
    }

    private void CheckUIElements()
    {
        if (xpBarText == null) Debug.LogError("xpBarText is not assigned.");
        if (ypBarText == null) Debug.LogError("ypBarText is not assigned.");
        if (levelText == null) Debug.LogError("levelText is not assigned.");
        if (warningText == null) Debug.LogError("warningText is not assigned.");
        if (softwareText == null) Debug.LogError("softwareText is not assigned.");
        if (managementText == null) Debug.LogError("managementText is not assigned.");
        if (saveButton == null) Debug.LogError("saveButton is not assigned.");
        if (loadButton == null) Debug.LogError("loadButton is not assigned.");
    }
}

public class UIUpdater : IUIUpdater
{
    private TextMeshProUGUI xpBarText;
    private TextMeshProUGUI ypBarText;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI softwareText;
    private TextMeshProUGUI managementText;
    private TextMeshProUGUI warningText;

    public UIUpdater(TextMeshProUGUI xpBarText, TextMeshProUGUI ypBarText, TextMeshProUGUI levelText,
                     TextMeshProUGUI softwareText, TextMeshProUGUI managementText, TextMeshProUGUI warningText)
    {
        this.xpBarText = xpBarText;
        this.ypBarText = ypBarText;
        this.levelText = levelText;
        this.softwareText = softwareText;
        this.managementText = managementText;
        this.warningText = warningText;
    }

    public void UpdateUI(PlayerStatsController playerStatsController)
    {
        if (playerStatsController == null)
        {
            Debug.LogError("PlayerStatsController is null in UpdateUI.");
            return;
        }

        xpBarText.text = playerStatsController.xpBar ?? "N/A";
        ypBarText.text = playerStatsController.ypBar ?? "N/A";
        levelText.text = playerStatsController.level ?? "N/A";
    }

    public void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
        }
        else
        {
            Debug.LogError("warningText is not assigned in ShowWarning.");
        }
    }

    public void HideWarning()
    {
        if (warningText != null)
        {
            warningText.text = "";
        }
        else
        {
            Debug.LogError("warningText is not assigned in HideWarning.");
        }
    }
}
