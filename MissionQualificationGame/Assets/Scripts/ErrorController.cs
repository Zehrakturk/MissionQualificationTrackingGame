using System.Collections;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    public GameObject missionPanel;
    public GameObject errorPanel;

    void Start()
    {
        if (errorPanel != null)
        {
            errorPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Error Paneli is not assigned.");
        }
    }

    public void ShowErrorPanel()
    {
        // Ensure the error panel exists before attempting to show it
        if (errorPanel == null)
        {
            errorPanel = GameObject.Find("error_panel");
        }

        if (errorPanel != null)
        {
            errorPanel.SetActive(true); // Show the error panel
            if (missionPanel != null)
            {
                missionPanel.SetActive(false);
            }
            StartCoroutine(HideErrorPanelAfterDelay(5f)); // 5 seconds delay before hiding the error panel
        }
        else
        {
            Debug.LogError("Error Paneli is not found.");
        }
    }

    private IEnumerator HideErrorPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check again before attempting to hide
        if (errorPanel != null)
        {
            errorPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Error Paneli was destroyed or missing when trying to hide.");
        }
    }
}
