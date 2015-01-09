using UnityEngine;
using System.Collections;

public class PlayerKill : MonoBehaviour {
	
	private Animator animGUI;
	private Animator animHero;
	private AudioSource[] sounds;
	private AudioSource death;
	
	private GameObject[] layers;

	void Start(){
		sounds = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();
		death = sounds[1];
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if(PlayerControl.playerIsDead == false){

				PlayerControl.playerIsDead = true;
				animGUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<Animator>();
				animGUI.SetTrigger("GameOver");

				other.GetComponent<PlayerControl>().moveForce = 0;
				animHero = other.GetComponent<Animator>();
				animHero.SetTrigger("Dead");

				Camera.main.GetComponent<PlayerFollow>().enabled = false;
				PlayerControl.isAlive = false;

				layers = GameObject.FindGameObjectsWithTag("Background");
				for(int i = 0; i<layers.Length; i++){
					layers[i].GetComponent<BackgroundScroller>().enabled = false;
				}
				death.Play();
			}
		}else if(other.tag != "destructor"){
			Destroy(other.gameObject);
		}    
	}
}