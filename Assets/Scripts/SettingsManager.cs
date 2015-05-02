using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {


	[SerializeField]
	public Toggle cbMusic;
	
	[SerializeField]
	public Toggle cbSFX;

	public static bool isMusic = true;
	public static bool isSFX = true;

	private AudioSource music;

	void Awake() {

		music = GameObject.FindGameObjectWithTag ("Music").GetComponent<AudioSource>();
		if (PlayerPrefs.GetFloat("MusicVolume") == 0f)
			cbMusic.isOn = false;
		music.volume = PlayerPrefs.GetFloat ("MusicVolume");

		if (PlayerPrefs.GetFloat("SoundVolume") == 0f)
			cbSFX.isOn = false;
	}

	void Update() {
		if(cbMusic.isOn) {
			music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
			music.volume = 1.0f;
			PlayerPrefs.SetFloat("MusicVolume", 1.0f);
		}
		else {
			music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
			music.volume = 0f;
			PlayerPrefs.SetFloat("MusicVolume", 0f);
		}

		if(cbSFX.isOn) {
			PlayerPrefs.SetFloat("SoundVolume", 1.0f);
		}
		else {
			PlayerPrefs.SetFloat("SoundVolume", 0f);
		}
	}
	

	public void OnMainMenu() {
		Application.LoadLevel ("MainMenu");
	}
	

	
}
