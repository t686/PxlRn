using UnityEngine;
using System.Collections;

public class PlayerKill : MonoBehaviour {
	
	private Animator animGUI;
	private Animator animHero;
	private AudioSource[] sounds;
	private AudioSource death;
	private PlayerControl playerScript;


	private GameObject[] backgroundLayers;


	void Start(){
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		backgroundLayers = GameObject.FindGameObjectsWithTag("Background");


		animHero = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		animGUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<Animator>();

		sounds = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();
		death = sounds[1];
		death.volume = PlayerPrefs.GetFloat ("SoundVolume")*0.7f;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if(!PlayerControl.playerIsDead == true){
				
				PlayerControl.playerIsDead = true;
				PlayerControl.tilesPassed = 0;

				animGUI.SetTrigger("GameOver");

				playerScript.moveForce = 0;
				animHero.SetTrigger("Dead");

				Camera.main.GetComponent<PlayerFollow>().enabled = false;
				//playerScript.enabled = false;

				PlayerControl.playerIsRunning = false;
				GameManager.tutorialDone = false;
				GamePause.pauseButton.SetActive(false);

				for(int i = 0; i<backgroundLayers.Length; i++){
					backgroundLayers[i].GetComponent<BackgroundScroller>().enabled = false;
				}
				death.Play();

				//GameManager.shareButton.SetActive(true);



			}
		}else if(other.tag != "destructor"){
			Destroy(other.gameObject);
		}    
	}
}