using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel;
    public Button button;
    private bool isToggling = false;

    void Start()
    {
        // Ba�lang��ta paneli inaktif yap
        if (panel != null)
        {
            panel.SetActive(false); // Panel ba�larken kapal� olacak
            Debug.Log("Panel ba�lang��ta inaktif yap�ld�.");
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
        if (isToggling) return; // E�er fonksiyon zaten �al���yorsa, ��k
        isToggling = true;

        if (panel != null)
        {
            // Panelin aktiflik durumunu tersine �evir
            panel.SetActive(!panel.activeSelf);
            Debug.Log("Panel durumu de�i�tirildi: " + panel.activeSelf);
        }
        else
        {
            Debug.LogError("Panel is not assigned.");
        }

        isToggling = false; // Fonksiyon tamamland���nda, tekrar �a�r�labilir hale getir
    }
}
