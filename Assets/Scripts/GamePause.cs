using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GamePause : MonoBehaviour {

	public float TimeScale;
	public bool paused;
	public GameObject guiMenu;	//Menu to Hide/Show onPause

	private GameObject[] layers;
	private AudioSource music;

	public static GameObject pauseButton; //Reference to the Pause button from GUI



	void Awake () {
		layers = GameObject.FindGameObjectsWithTag("Background");
		pauseButton = GameObject.Find("PauseButton");
		pauseButton.SetActive(false);

		guiMenu = GameObject.Find("GameMenu");
		guiMenu.SetActive(false);
	}

	void Start () {
		TimeScale = Time.timeScale;//1


	}

	void Update () {
		if(!music) music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

	
		if(paused){
			if(Time.timeScale > 0.0){
				Time.timeScale = 0.0f;
				//music.Pause();
				for(int i = 0; i<layers.Length; i++){
					layers[i].GetComponent<BackgroundScroller>().enabled = false;
				}
			}

		}else {
			if(Time.timeScale < TimeScale){
				Time.timeScale = TimeScale;
				//music.Play();
				if(PlayerControl.playerIsDead == false){
					for(int i = 0; i<layers.Length; i++){
						layers[i].GetComponent<BackgroundScroller>().enabled = true;
					}
				}
			}
		}
	}

	public void PauseTrigger(){
		if(!paused){
			paused = true;
			guiMenu.SetActive(true);
			pauseButton.SetActive(false);
		}
		else{
			paused = false;
			guiMenu.SetActive(false);
			pauseButton.SetActive(true);
		}

	}
}
