using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public GameObject cutScene;
	private AudioSource mMusic;

	void Awake() {

		mMusic = GameObject.FindGameObjectWithTag ("Music").GetComponent<AudioSource>();
		mMusic.volume = PlayerPrefs.GetFloat ("MusicVolume");
	}

	public void StartButton(){
		Color cutSceneOpacity = cutScene.GetComponent<SpriteRenderer>().color;
		cutSceneOpacity.a = 1;
		cutScene.GetComponent<SpriteRenderer>().color = cutSceneOpacity;
		GameObject.FindGameObjectWithTag("GUI").SetActive(false);
		Invoke("ChangeLevel", 3.0f);
	}

	void ChangeLevel(){
		Application.LoadLevel("GameLevel01");
	}

	public void OnSettings() {
		Application.LoadLevel ("Settings");
	}

	public void OnAbout() {
		Application.LoadLevel ("About");
	}
}
