using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class yukari : MonoBehaviour
{
    public int sceneBuildIndex;          // Hedef sahne index'i
    private GameObject player;           // Oyuncu GameObject'i
    public Animator transitionAnimator;  // Sahne geçiþ animasyonunun Animator bileþeni
    public string closeTrigger = "End";  // Scene kapanýþ trigger ismi
    public string openTrigger = "Start"; // Scene açýlýþ trigger ismi
    public float transitionTime = 1f;    // Animasyon süresi

    private void Start()
    {
        // Oyuncu nesnesini sahnede bul
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Oyuncu nesnesinin sahne geçiþlerinde yok edilmemesini saðla
            DontDestroyOnLoad(player);
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the 'Player' tag.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Switching Scene to " + sceneBuildIndex);
            StartCoroutine(SwitchSceneAndRepositionPlayer());
        }
    }

    private IEnumerator SwitchSceneAndRepositionPlayer()
    {
        // Sahne kapanýþ animasyonunu baþlat
        if (transitionAnimator != null)
        {
            Debug.Log("Starting close animation");
            transitionAnimator.SetTrigger(closeTrigger);
        }

        // Animasyonun bitmesini bekle
        yield return new WaitForSeconds(transitionTime);

        // Yeni sahneyi asenkron olarak yükle
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Oyuncuyu yeni sahnede yeniden konumlandýr
        RepositionPlayerInNewScene();

        // Sahne açýlýþ animasyonunu baþlat
        if (transitionAnimator != null)
        {
            Debug.Log("Starting open animation");
            transitionAnimator.SetTrigger(openTrigger);

            // Animasyonun bitmesini bekle
            yield return new WaitForSeconds(transitionTime);
        }

        // Virtual Camera'yý yeniden ayarladýðýnda oyunu exe haline getirdiðimizde ikinci sahnede playerý takip etmiyor.
        //ReassignVirtualCameraTarget();
    }

    private void RepositionPlayerInNewScene()
    {
        // Yeni sahnedeki oyuncu baþlangýç konumunu bul
        GameObject playerStart = GameObject.FindGameObjectWithTag("PlayerSpawner");

        if (playerStart != null)
        {
            // Oyuncuyu yeniden konumlandýr
            player.transform.position = playerStart.transform.position;
            Debug.Log("Player repositioned to " + player.transform.position);
        }
        else
        {
            Debug.LogWarning("PlayerStart not found in the new scene. Make sure you have a GameObject tagged as 'PlayerStart'.");
        }
    }

    private void ReassignVirtualCameraTarget()
    {
        // Virtual Camera'yý bul ve hedefini ayarla
        var virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        var player = GameObject.FindGameObjectWithTag("Player");

        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
            Debug.Log("Virtual Camera target reassigned to player.");
        }
        else
        {
            Debug.LogError("Virtual Camera not found in the new scene.");
        }
    }
}
        