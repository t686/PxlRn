using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour {

	public float TimeScale;
	public bool paused;
	private GameObject[] layers;
	private AudioSource music;

	void Awake () {
		layers = GameObject.FindGameObjectsWithTag("Background");
	}

	void Start () {
		TimeScale = Time.timeScale;//1


	}

	void Update () {
		if(!music) music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

		if(Input.GetKeyDown("escape")){
			if(!paused) paused = true;
			else paused = false;
		}

		if(paused){
			if(Time.timeScale > 0.0){
				Time.timeScale = 0.0f;
				music.Pause();
				for(int i = 0; i<layers.Length; i++){
					layers[i].GetComponent<BackgroundScroller>().enabled = false;
				}
				//Show up GameMenu
			}

		}else {
			if(Time.timeScale < TimeScale){
				Time.timeScale = TimeScale;
				music.Play();
				if(PlayerControl.playerIsDead == false){
					for(int i = 0; i<layers.Length; i++){
						layers[i].GetComponent<BackgroundScroller>().enabled = true;
					}
				}
			}
		}
	}
}
