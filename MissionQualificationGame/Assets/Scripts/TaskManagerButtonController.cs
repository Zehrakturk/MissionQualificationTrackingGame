using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MissionManagerButtonController : MonoBehaviour
{
    public Button mainButton;
    public Button exitButton;
    public GameObject panelPrefab;
    public TextMeshProUGUI prefabText;
    public Button nextButton;
    public Button backButton;
    public Transform panelContainer; // Container for panels
    public List<TextMeshProUGUI> textMeshProUGUIs;

    private List<GameObject> panelList = new List<GameObject>();
    private int currentIndex = 0; // Tracks the index of the panel to be shown

    void Start()
    {
        panelPrefab.SetActive(false); // Hide prefab panel initially

        mainButton.onClick.AddListener(ShowMainPanel);
        nextButton.onClick.AddListener(NextPanel);
        exitButton.onClick.AddListener(ExitPanel);
        backButton.onClick.AddListener(BackPanel);
    }

    public void ShowMainPanel()
    {
        panelList.Add(panelPrefab);
        textMeshProUGUIs.Add(prefabText);
        panelPrefab.SetActive(true);
    }

    public void NextPanel()
    {
        if (panelList.Count > 0 && currentIndex < panelList.Count - 1)
        {
            //HideCurrentPanelComponents();
            currentIndex++;
            UpdatePanelVisibility();
            //EnablePanelComponents(panelList[currentIndex]); // Geçerli panelin komponentlerini etkinleþtir
            panelList[currentIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No next panel available or index is out of range.");
        }
    }

    public void BackPanel()
    {
        if (panelList.Count > 0 && currentIndex > 0)
        {
            currentIndex = Mathf.Clamp(currentIndex - 1, 0, panelList.Count - 1); // Ensure valid index
            currentIndex--;
            UpdatePanelVisibility();
            //EnablePanelComponents(panelList[currentIndex]); // Geçerli panelin komponentlerini etkinleþtir
            panelList[currentIndex].SetActive(true);
        }
        else if (currentIndex == 0) // Show main panel on first back press
        {
            ShowMainPanel();
        }
        else
        {
            Debug.LogError("Index is negative or out of range.");
        }
    }

    private void UpdatePanelVisibility()
    {
        // Ensure only the current panel is active
 
            for (int i = 0; i < textMeshProUGUIs.Count; i++)
            {
                if (i == currentIndex)
                {
                    textMeshProUGUIs[i].enabled = true;
                }
               
                //panelList[i].SetActive(!isActivePanel);
                
                //if (!isActivePanel)
                //{
                //    DisablePanelComponents(panelList[i]);
                //}
                //else
                //{
                //    EnablePanelComponents(panelList[i]);
                //}
            }

            
        
    }

    private void DisablePanelComponents(GameObject panel)
    {
        foreach (Transform child in panel.transform)
        {
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }

            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                image.gameObject.SetActive(false);
            }

            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.gameObject.SetActive(true);
                
            }
        }
    }

    private void EnablePanelComponents(GameObject panel)
    {
        foreach (Transform child in panel.transform)
        {
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                text.gameObject.SetActive(true);
            }

            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                image.gameObject.SetActive(true);
            }

            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.gameObject.SetActive(true);
            }
        }
    }


    public void ExitPanel()
    {
        panelPrefab.SetActive(false);
        currentIndex = 0;
        for (int i = 0; i < panelList.Count; i++)
        {
            panelList[i].SetActive(false);
        }
    }

    public void AddPanel(TextMeshProUGUI text)
    {
        text.transform.SetSiblingIndex(currentIndex);
        textMeshProUGUIs.Add(text);



        //panel.transform.SetParent(panelContainer, false); // Add panel to container

        //RectTransform panelRect = panel.GetComponent<RectTransform>();
        //RectTransform prefabRect = panelPrefab.GetComponent<RectTransform>();
        //panelRect.sizeDelta = prefabRect.sizeDelta / 2; // Set panel size
        //panelRect.localScale = prefabRect.localScale;

        //// Handle confirm button (optional)
        //Button confirmButton = panel.transform.Find("Button").GetComponent<Button>();
        //if (confirmButton != null)
        //{
        //    confirmButton.gameObject.IsDestroyed(); // Deactivate confirm button (example)
        //    // You can customize confirm button behavior here
        //}
        //else
        //{
        //    Debug.LogWarning("ConfirmButton not found in the panel.");
        //}

        //panelRect.anchoredPosition += new Vector2(-150, 0); // Offset panel position

        //// Set initial text color (optional)
        //TextMeshPro[] textComponents = panel.GetComponentsInChildren<TextMeshPro>();
        //foreach (TextMeshPro textComponent in textComponents)
        //{
        //    textComponent.color = Color.black;
        //}

        //panelList.Add(panel);
        
    }
}
