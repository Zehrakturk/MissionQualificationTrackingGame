using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapi : MonoBehaviour
{
    public Animator animator; // Kapýyý açmak ve kapatmak için Animator bileþenini kullanacaðýz
    private bool isOpen = true; // Kapýnýn açýk olup olmadýðýný kontrol etmek için
    private Collider2D doorCollider; // Kapýnýn 2D Collider bileþeni
    private bool playerInRange = false; // Oyuncunun kapýnýn yakýnýnda olup olmadýðýný kontrol etmek için

    void Start()
    {
        // Animator ve Collider bileþenlerini al
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();

        // Kapý kapalýyken Collider'ý etkinleþtir
        doorCollider.enabled = true;

    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    void OpenDoor()
    {
        animator.SetBool("isOpen", true); // Animator'da "isOpen" parametresini true yaparak kapýyý aç
        doorCollider.enabled = true; // Kapý açýldýðýnda Collider'ý devre dýþý býrak
        isOpen = true;
    }

    void CloseDoor()
    {
        animator.SetBool("isOpen", false); // Animator'da "isOpen" parametresini false yaparak kapýyý kapat
        doorCollider.enabled = false; // Kapý kapandýðýnda Collider'ý etkinleþtir
        isOpen = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Oyuncu kapýnýn yakýnýnda
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Oyuncu kapýnýn yakýnýnda deðil
        }
    }
}