using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static bool tutorialDone = false;
	
	public GameObject music;	  			//The level music object
	
	private GameObject isMusic;	  			//To check if the music object exists
	private GameObject[] backgroundLayers;	//Variables for activating the layout scripts
	
	void Awake(){
		backgroundLayers = GameObject.FindGameObjectsWithTag("Background");
	}
	
	void Start(){
		
		//Create a music object is one if absent
		isMusic = GameObject.FindGameObjectWithTag("Music");
		if(!isMusic){
			Instantiate(music, Vector3.zero, Quaternion.identity);
		}
	}
	
	void FixedUpdate () {
		//start level after jump
		if(Input.GetButtonDown("Jump") && tutorialDone == false && PlayerControl.playerIsDead == false){
			
			GamePause.pauseButton.SetActive(true);
			tutorialDone = true; 
			PlayerControl.playerIsRunning = true;
			
			
			//start the layers parallax scripts
			for(int i = 0; i<backgroundLayers.Length; i++){
				backgroundLayers[i].GetComponent<BackgroundScroller>().enabled = true;
			}
		}
		
		
		//Restart the game after a "Tap" if a player is previously dead
		if(PlayerControl.playerIsDead){
			if(Input.GetButtonDown("Jump")){
				Invoke ("RestartLevel", PlayerControl.restartDelay);
			}
		}
		
	}
	
	void RestartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	//GUI button Restart
	public void OnRestart(){
		RestartLevel();
	}

	//GUI button Exit
	public void OnExit(){
		Destroy(isMusic);
		Application.LoadLevel("MainMenu");
	}
}