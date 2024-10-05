using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelirteciKontrol : MonoBehaviour
{
    public GameObject kapý; // Bu belirteçle iliþkili kapý
    private Kapi kapiScript;
    public bool playerInRange = false;

    void Start()
    {
        // Kapýdaki Kapi scriptini al
        if (kapý != null)
        {
            kapiScript = kapý.GetComponent<Kapi>();
        }
    }

    // Oyuncu belirtecin içine girdiðinde
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // Oyuncu belirtecin dýþýna çýktýðýnda
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
