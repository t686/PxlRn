using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static bool tutorialDone = false;
	
	public GameObject music;	  			//The level music object
	public GameObject[] tileSpawners;		//Array containing references to all tileSpawners objects, because they are inactive

	private GameObject isMusic;	  			//To check if the music object exists
	private TileSpawn tileSpawnScript;
	private GameObject[] backgroundLayers;	//Variables for activating the layout scripts
	private int settingID;



	void Awake(){
		//Get the `random` setting for level
		SetEnvironment(tileSpawners);

		backgroundLayers = GameObject.FindGameObjectsWithTag("Background");
		//Create a music object if one is absent
		isMusic = GameObject.FindGameObjectWithTag("Music");

		//Check Music settings
		music.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("MusicVolume")*0.35f;
	}
	
	void Start(){
		//Before the level started create a default tile
		GameObject tileSpawnObject = GameObject.FindWithTag("TileSpawner");
		tileSpawnScript = tileSpawnObject.GetComponent<TileSpawn>();
		tileSpawnScript.SpawnDefault();


		if(!isMusic){
			Instantiate(music, Vector3.zero, Quaternion.identity);
		}
		isMusic = GameObject.FindGameObjectWithTag("Music");

	}
	
	void FixedUpdate () {
		//start level after Tutorial passed
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

	private void SetEnvironment(GameObject[] tileSpawners){
		settingID = Random.Range(0,2); //So far 0 or 1 because only 2 Enviroments are available 
		Debug.Log("Setting ID: "+settingID);

		switch (settingID){
		case 0:
			print ("Forrest environmet activated");
			tileSpawners[settingID].SetActive(true);
			break;
		case 1:
			print ("Desert environmet activated");
			tileSpawners[settingID].SetActive(true);
			break;
		case 2:
			print ("Space environmet activated");
			tileSpawners[settingID].SetActive(true);
			break;
		case 3:
			print ("Vulcano environmet activated");
			tileSpawners[settingID].SetActive(true);
			break;
		case 4:
			print ("Winter environmet activated");
			tileSpawners[settingID].SetActive(true);
			break;
		default:
			print ("Default environmet activated");
			break;
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