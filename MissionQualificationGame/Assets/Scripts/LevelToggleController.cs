using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelToggleController : MonoBehaviour
{
    public Image levelBar; // Refer to your level bar Image component
    private ToggleManager toggleManager; // Reference to the ToggleManager script
    private float changeAmount = 0.05f; // Amount by which the level bar changes (5%)
    private bool previousToggleStatus; // Previous status of the toggle
    public TextMeshProUGUI percentageText; // Refer to the TextMeshProUGUI component
    public CelebrationController celebrationController; // Refer to the CelebrationController
    public ErrorController errorController; // Refer to the ErrorController
    private GizliGorev gizliGorev; // gorev_controller referansý
    public Image softwareSkillBar;
    public TextMeshProUGUI softwareSkillText;
    public Image managemenetSkillBar;
    public TextMeshProUGUI managemenetSkillText;

    private bool isConfirmed = false; // Flag to ensure OnConfirmButton is executed only once

    void Start()
    {
        // Baþlangýçta percentageText'i doðru seviyeden baþlat
        UpdatePercentageText();
    }

    public void OnConfirmButton()
    {
        celebrationController = FindObjectOfType<CelebrationController>();
        errorController = FindObjectOfType<ErrorController>();
        toggleManager = FindObjectOfType<ToggleManager>();
        gizliGorev = FindObjectOfType<GizliGorev>(); // gorev_controller'ý bul

        if (isConfirmed) return; // Exit method if already confirmed

        isConfirmed = true; // Set the flag to true

        gizliGorev.confirmButton.enabled = false;

        if (toggleManager == null)
        {
            Debug.LogError("Toggle manager yok");
            return;
        }

        bool currentToggleStatus = toggleManager.CheckToggleStatus();

        if (currentToggleStatus)
        {
            IncreaseBar();
        }
        else
        {
            DecreaseBar();
        }

        previousToggleStatus = currentToggleStatus; // Update the status
    }

    void IncreaseBar()
    {
        levelBar.fillAmount += changeAmount;
        if (levelBar.fillAmount > 1f) // Max value example
        {
            levelBar.fillAmount = 1f;
        }
        UpdatePercentageText();
        UpdateUserSkills();
        celebrationController.ShowCelebrationPanel();
    }

    void DecreaseBar()
    {
        levelBar.fillAmount -= changeAmount;
        if (levelBar.fillAmount < 0f)
        {
            levelBar.fillAmount = 0f;
        }
        UpdatePercentageText();
        errorController.ShowErrorPanel();
    }

    public void UpdatePercentageText()
    {
        percentageText.text = "%" + ((int)(levelBar.fillAmount * 100f)).ToString();
        Debug.Log("Updated percentage text: " + percentageText.text);
    }

    // Call this method to reset the confirmation flag when needed (e.g., after a certain period)
    public void ResetConfirmation()
    {
        isConfirmed = false;
    }

    private void UpdateUserSkills()
    {
        // Get reference to the TaskJsonController
        TaskJsonController taskJsonController = FindObjectOfType<TaskJsonController>();

        if (taskJsonController != null)
        {
            // Get the skills from TaskJsonController's current task
            var taskSkills = taskJsonController.currentTask.skillList; // currentTask yerine skillList kullan

            float increaseAmountSoftwareLevel = int.Parse(taskSkills[0].ToString()) / 100f; // 100'e bölerek 0-1 aralýðýna getiriyoruz
            softwareSkillBar.fillAmount += increaseAmountSoftwareLevel;
            softwareSkillText.text = "%" + ((int)(softwareSkillBar.fillAmount * 100f)).ToString();

            float increaseAmountManagementLevel = int.Parse(taskSkills[1].ToString()) / 100f; // 100'e bölerek 0-1 aralýðýna getiriyoruz
            managemenetSkillBar.fillAmount += increaseAmountManagementLevel;
            managemenetSkillText.text = "%" + ((int)(managemenetSkillBar.fillAmount * 100f)).ToString(); // Burada doðru fillAmount kullanýlýyor
        }
    }

}
