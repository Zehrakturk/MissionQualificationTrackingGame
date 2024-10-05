using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class asagi : MonoBehaviour
{
    public int sceneBuildIndex;
    private GameObject player;

    private void Start()
    {
        // Find the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Ensure the player object is not destroyed on scene load
            DontDestroyOnLoad(player);
            Debug.Log("Player found and DontDestroyOnLoad set.");
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
            // Player entered, so move level
            Debug.Log("Switching Scene to " + sceneBuildIndex);
            StartCoroutine(SwitchSceneAndRepositionPlayer());
        }
    }

    private IEnumerator SwitchSceneAndRepositionPlayer()
    {
        // Load the new scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

        // Wait until the new scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Reposition the player in the new scene
        RepositionPlayerInNewScene();

        // Virtual Camera'yý yeniden ayarla
        ReassignVirtualCameraTarget();
    }

    private void RepositionPlayerInNewScene()
    {
        // Find the player start position in the new scene
        GameObject playerStart = GameObject.FindGameObjectWithTag("PlayerSpawner");

        if (playerStart != null)
        {
            // Reposition the player
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
        var virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;
            Debug.Log("Virtual Camera target reassigned to player.");
        }
        else
        {
            Debug.LogError("Virtual Camera not found in the new scene.");
        }
    }
}