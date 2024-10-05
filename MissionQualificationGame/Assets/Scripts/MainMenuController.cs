using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class MainMenuController : MonoBehaviour
{
	public Button startButton;
	public Button optionsButton;
	public Button messageButton;
	public Button quitButton;
	public UnityEngine.UI.Button closeButton;
	public Label messageText;
	public GameObject UIDocument;
	private GameObject[] keybindButtons;

	public btnFX buttonFX; // btnFX script'ini referans olarak ekle

	// Start is called before the first frame update
	void Start()
	{
		// Ýkinci bir EventSystem var mý kontrol et ve yok et
		RemoveExtraEventSystems();

		var root = GetComponent<UIDocument>().rootVisualElement;

		startButton = root.Q<Button>("Start-button");
		optionsButton = root.Q<Button>("Options-button");
		messageButton = root.Q<Button>("Talent-astra");
		quitButton = root.Q<Button>("Quit-button");

		// Butonlara ses efektlerini ekle
		startButton.RegisterCallback<MouseEnterEvent>(ev => OnButtonHover());
		startButton.clicked += StartButtonPressed;
		optionsButton.RegisterCallback<MouseEnterEvent>(ev => OnButtonHover());
		optionsButton.clicked += OptionsButtonPressed;
		messageButton.RegisterCallback<MouseEnterEvent>(ev => OnButtonHover());
		messageButton.clicked += MessageButtonPressed;
		quitButton.RegisterCallback<MouseEnterEvent>(ev => OnButtonHover());
		quitButton.clicked += QuitButtonPressed;

		closeButton.onClick.AddListener(CloseButtonPressed);
	}

	void StartButtonPressed()
	{
		buttonFX.ClickSound();
		SceneManager.LoadScene("CutScene");
	}

	void OptionsButtonPressed()
	{
		buttonFX.ClickSound();
		EnsureEventSystemExists();
		UIDocument.SetActive(false);
		Debug.Log("Options Button Pressed");
	}

	public void CloseButtonPressed()
	{
		buttonFX.ClickSound();
		SceneManager.LoadScene("mainMenu");
	}

	void MessageButtonPressed()
	{
		buttonFX.ClickSound();
		Application.OpenURL("https://yetkinlikyonetimi.com/");
	}

	void QuitButtonPressed()
	{
		buttonFX.ClickSound();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	private void OnButtonHover()
	{
		buttonFX.HoverSound();
	}

	// EventSystem kontrolü ve oluþturulmasý
	private void EnsureEventSystemExists()
	{
		var eventSystems = FindObjectsOfType<UnityEngine.EventSystems.EventSystem>();
		if (eventSystems.Length == 0)
		{
			// EventSystem yoksa oluþtur
			var go = new GameObject("EventSystem");
			go.AddComponent<UnityEngine.EventSystems.EventSystem>();
			go.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
		}
		else if (eventSystems.Length > 1)
		{
			// Çoklu EventSystem varsa sil
			for (int i = 1; i < eventSystems.Length; i++)
			{
				Destroy(eventSystems[i].gameObject);
			}
		}
	}

	//keybind menu kodlarý
	public void UpdateKeyText(string key, KeyCode code)
	{
		TextMeshProUGUI tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<TextMeshProUGUI>();
		tmp.text = code.ToString();
	}

	private static MainMenuController instance;

	public static MainMenuController MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<MainMenuController>();
			}

			return instance;
		}
	}

	private void Awake()
	{
		keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
	}

	private void RemoveExtraEventSystems()
	{
		var eventSystems = FindObjectsOfType<UnityEngine.EventSystems.EventSystem>();
		if (eventSystems.Length > 1)
		{
			for (int i = 1; i < eventSystems.Length; i++)
			{
				Destroy(eventSystems[i].gameObject);
			}
		}
	}
}