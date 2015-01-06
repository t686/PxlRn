using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour {

	public float TimeScale;
	public bool paused;
	private GameObject[] layers;

	void Awake () {
		layers = GameObject.FindGameObjectsWithTag("Background");
	}

	void Start () {
		TimeScale = Time.timeScale;//1
	}

	void Update () {
		if(Input.GetKeyDown("escape")){
			if(!paused){
				paused = true;
			}else{
				paused = false;
			}
		}

		if(paused){
			if(Time.timeScale > 0.0){
				Time.timeScale = 0.0f;
				audio.Pause();
				for(int i = 0; i<layers.Length; i++){
					layers[i].GetComponent<BackgroundScroller>().enabled = false;
				}
				//Show up GameMenu
			}

		}else {
			if(Time.timeScale < TimeScale){
				Time.timeScale = TimeScale;
				audio.Play();
				if(PlayerControl.playerIsDead == false){
					for(int i = 0; i<layers.Length; i++){
						layers[i].GetComponent<BackgroundScroller>().enabled = true;
					}
				}
			}
		}
	}
}
