using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	//public AudioSource[] musicThemes;
	//private AudioSource level1Theme;
	//private AudioSource level2Theme;
	//private AudioSource level3Theme;
	//private AudioSource MenuTheme;

	// Use this for initialization
	//void Awake() {
		//musicThemes = this.GetComponents<AudioSource>();
		//
	//}

	void Start () {
		DontDestroyOnLoad(gameObject);
	}
}
