using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel;
    public Button button;
    private bool isToggling = false;

    void Start()
    {
        // Baþlangýçta paneli inaktif yap
        if (panel != null)
        {
            panel.SetActive(false); // Panel baþlarken kapalý olacak
            Debug.Log("Panel baþlangýçta inaktif yapýldý.");
        }
        else
        {
            Debug.LogError("Panel is not assigned.");
        }

        // Butonun onClick eventine TogglePanel fonksiyonunu ekle
        if (button != null)
        {
            button.onClick.AddListener(TogglePanel);
            Debug.Log("Buton listener eklendi.");
        }
        else
        {
            Debug.LogError("Button is not assigned.");
        }
    }

    public void TogglePanel()
    {
        if (isToggling) return; // Eðer fonksiyon zaten çalýþýyorsa, çýk
        isToggling = true;

        if (panel != null)
        {
            // Panelin aktiflik durumunu tersine çevir
            panel.SetActive(!panel.activeSelf);
            Debug.Log("Panel durumu deðiþtirildi: " + panel.activeSelf);
        }
        else
        {
            Debug.LogError("Panel is not assigned.");
        }

        isToggling = false; // Fonksiyon tamamlandýðýnda, tekrar çaðrýlabilir hale getir
    }
}
