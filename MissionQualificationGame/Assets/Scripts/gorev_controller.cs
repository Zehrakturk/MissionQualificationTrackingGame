using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gorev_controller : MonoBehaviour
{
    public GameObject gorevBalonu;
    public GameObject panel;
    public CapsuleCollider2D capsuleCollider;
    private bool panelOpened = false; // Panelin a��l�p a��lmad���n� kontrol eden flag
    private MissionManagerButtonController missionManager; // MissionManagerButtonController referans�

    void Start()
    {
        missionManager = FindObjectOfType<MissionManagerButtonController>();
        // Paneli ba�lang��ta kapal� yap
        panel.SetActive(false);
    }

    // Collider'a ba�ka bir obje girdi�inde �al���r
    void OnTriggerEnter2D(Collider2D other)
    {
        // Panel daha �nce a��ld�ysa hi�bir �ey yapma
        if (panelOpened)
        {
            return;
        }

        // Oyuncu g�rev balonuna girdi�inde paneli a�
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            panelOpened = true; // Panel a��ld� olarak i�aretle
        }
    }

    // Collider'dan ba�ka bir obje ��kt���nda �al���r
    void OnTriggerExit2D(Collider2D other)
    {
        // Oyuncu g�rev balonundan ��kt���nda paneli kapat
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            capsuleCollider.enabled = false;
            gorevBalonu.SetActive(false);
        }
    }

    // Confirm butonuna t�klanarak �a�r�lacak fonksiyon
    public void OnConfirmButtonClicked()
    {
        panelOpened = true; // Panelin a��ld���n� i�aretle
        panel.SetActive(false); // Paneli kapat
        gorevBalonu.SetActive(false);
        capsuleCollider.enabled = false; // Collider'� devre d��� b�rak

        // MissionManagerButtonController'daki AddPanel metodunu �a��r
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
