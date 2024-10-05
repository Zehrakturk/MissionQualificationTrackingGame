using System.Collections;
using UnityEngine;

public class CelebrationController : MonoBehaviour
{
    public GameObject missionPanel;
    public GameObject celebrationPanel;

    void Start()
    {
        celebrationPanel.SetActive(false);
    }

    public void ShowCelebrationPanel()
    {
        if (celebrationPanel != null)
        {
            celebrationPanel.SetActive(true); // Show the celebration panel
            missionPanel.SetActive(false);
            StartCoroutine(HideCelebrationPanelAfterDelay(5f)); // 5 saniye sonra kutlama panelini kapat
        }
        else
        {
            Debug.LogError("Kutlama Paneli is not assigned.");
        }
    }

    private IEnumerator HideCelebrationPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        celebrationPanel.SetActive(false); // Hide the celebration panel
    }
}
