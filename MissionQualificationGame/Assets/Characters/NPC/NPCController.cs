using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public GameObject dialoguePanel; // Dialog paneli burada atanacak
    public GameObject gorevPanel; // G�rev paneli burada atanacak
    public GameObject konusmaPanel;
    public Button acceptButton;
    public Button konusmaButton;
    public Button exitButton;


    void Start()
    {
        // Panelleri ba�lang��ta gizle
        dialoguePanel.SetActive(false);
        gorevPanel.SetActive(false);
        konusmaPanel.SetActive(false);

        acceptButton.onClick.AddListener(ClosePanel);
        konusmaButton.onClick.AddListener(ToggleKonusmaPanel);
        exitButton.onClick.AddListener(konusmaPanelClose);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er oyuncu collider'a girdiyse
        if (other.CompareTag("Player"))
        {
            OpenPanel();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // E�er oyuncu collider'dan ��kt�ysa
        if (other.CompareTag("Player"))
        {
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        // Dialog panelini aktif hale getir
        dialoguePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        // Panelleri pasif hale getir
        //dialoguePanel.SetActive(false);
        gorevPanel.SetActive(true);
        Debug.Log("Panel kapanmad�");
    }

    public void ToggleKonusmaPanel()
    {
        // Flag'in durumunu de�i�tir
        konusmaPanel.SetActive(true);
    }

    public void konusmaPanelClose()
    {
        konusmaPanel.SetActive(false);
    }
}
