using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/**
 * Controls how the UI works for the interface
**/
namespace XML_Worker {
public class SaveUI : MonoBehaviour
{
	public Button addModsButton;
	public Button showSavesButton;
	public Button loadSelectedButton;
	public RectTransform gameSavesDisplayContainer;
	public Button gameSavesDisplayButtonPrefab;
	
	private Button currentSelectedButton;
	private Dictionary<Button, Pattern> patternButton = new Dictionary<Button, Pattern>();
	
	public Transform skinBox;
	private Renderer rend;
	
	void Awake()
	{
		addModsButton.onClick.AddListener(QueryMods);
		showSavesButton.onClick.AddListener(ShowSavedPatterns);
		loadSelectedButton.onClick.AddListener(LoadSelectedPattern);
		rend = skinBox.GetComponent<Renderer>();
	}
	
	void LoadSelectedPattern()
	{
		if(currentSelectedButton != null)
		{
			if (rend != null) {
				rend.material.SetTexture("_Pattern", patternButton[currentSelectedButton].material);
			} else
				Debug.LogWarning("Object texture not chosen.");
		}
	}
	
	void QueryMods() {
		PatternController.LoadMods();
	}
	
	void OnSkinChange()
	{
		if (currentSelectedButton != null)
			currentSelectedButton.GetComponent<Image>().color = Color.white;
		
		currentSelectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
		currentSelectedButton.GetComponent<Image>().color = Color.cyan;
	}

	void ShowSavedPatterns()
	{
		foreach(RectTransform rect in gameSavesDisplayContainer)
		{
			GameObject.Destroy(rect.gameObject);
		}

		List<Pattern> patterns = PatternController.RetrievePatterns();

		foreach(Pattern p in patterns)
		{
			Button button = GameObject.Instantiate(gameSavesDisplayButtonPrefab);
			button.GetComponentInChildren<Text>().text = p.Name + "\n" + p.Author;
			button.transform.SetParent(gameSavesDisplayContainer);
			button.onClick.AddListener(OnSkinChange);
			patternButton.Add(button, p);
		}
	}
}
}