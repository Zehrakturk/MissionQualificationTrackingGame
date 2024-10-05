using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GizliGorev : MonoBehaviour
{
    public GameObject gorevBalonu;
    public GameObject panel;
    public CapsuleCollider2D capsuleCollider;
    private bool panelOpened = false;
    private MissionManagerButtonController missionManager;
    public Button confirmButton;
    public bool isConfirmed = false;
    LevelToggleController levelToggleController;// Butonun týklanýp týklanmadýðýný izlemek için kullanýlan iþaretçi

    void Start()
    {
        if (panel == null)
        {
            Debug.LogError("Panel is not assigned in the Inspector");
            return;
        }

        missionManager = FindObjectOfType<MissionManagerButtonController>();
        levelToggleController= FindObjectOfType<LevelToggleController>();

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }
        else
        {
            Debug.LogError("Confirm button not found");
        }

        panel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (panelOpened)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !panelOpened)
        {
            panel.SetActive(false);
            capsuleCollider.enabled = false;
            confirmButton.enabled = false;
        }
    }

    public void OnConfirmButtonClicked()
    {
        if (panel != null)
        {
            panelOpened = true;
            panel.SetActive(false);
            capsuleCollider.enabled = false;
            isConfirmed = true;
            confirmButton.enabled = false;
            confirmButton.gameObject.SetActive(false); // Görünürlüðünü gizle
            levelToggleController.OnConfirmButton();
         
        }
        else
        {
            Debug.LogError("Panel has been destroyed or is null");
        }
    }
}
