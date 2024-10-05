using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gorev_controller : MonoBehaviour
{
    public GameObject gorevBalonu;
    public GameObject panel;
    public CapsuleCollider2D capsuleCollider;
    private bool panelOpened = false; // Panelin açýlýp açýlmadýðýný kontrol eden flag
    private MissionManagerButtonController missionManager; // MissionManagerButtonController referansý

    void Start()
    {
        missionManager = FindObjectOfType<MissionManagerButtonController>();
        // Paneli baþlangýçta kapalý yap
        panel.SetActive(false);
    }

    // Collider'a baþka bir obje girdiðinde çalýþýr
    void OnTriggerEnter2D(Collider2D other)
    {
        // Panel daha önce açýldýysa hiçbir þey yapma
        if (panelOpened)
        {
            return;
        }

        // Oyuncu görev balonuna girdiðinde paneli aç
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            panelOpened = true; // Panel açýldý olarak iþaretle
        }
    }

    // Collider'dan baþka bir obje çýktýðýnda çalýþýr
    void OnTriggerExit2D(Collider2D other)
    {
        // Oyuncu görev balonundan çýktýðýnda paneli kapat
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            capsuleCollider.enabled = false;
            gorevBalonu.SetActive(false);
        }
    }

    // Confirm butonuna týklanarak çaðrýlacak fonksiyon
    public void OnConfirmButtonClicked()
    {
        panelOpened = true; // Panelin açýldýðýný iþaretle
        panel.SetActive(false); // Paneli kapat
        gorevBalonu.SetActive(false);
        capsuleCollider.enabled = false; // Collider'ý devre dýþý býrak

        // MissionManagerButtonController'daki AddPanel metodunu çaðýr
        //if (missionManager != null)
        //{
        //    missionManager.AddPanel(panel);
        //}
        //else
        //{
        //    Debug.LogError("MissionManagerButtonController is not assigned.");
        //}
    }
}
