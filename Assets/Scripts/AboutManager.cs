using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AboutManager : MonoBehaviour {

	private AudioSource mMusic;

	void Awake() {
		mMusic = GameObject.FindGameObjectWithTag ("Music").GetComponent<AudioSource>();
		mMusic.volume = PlayerPrefs.GetFloat ("MusicVolume");
	}


	public void OnMainMenu() {
		Application.LoadLevel ("MainMenu");
	}

		
}
