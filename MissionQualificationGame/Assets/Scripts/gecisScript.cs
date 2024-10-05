using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gecisScript : MonoBehaviour
{ 

    void Start()
    {
        // 10 saniye sonra SampleScene sahnesine geçiþ yap
        Invoke("LoadSampleScene", 56f);
    }

    void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene("SampleScene");
        }
	}
}

