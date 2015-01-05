using UnityEngine;
using System.Collections;

public class PlayerKill : MonoBehaviour {
	
	private Animator animGUI;
	private Animator animHero;

	//public bool playerIsDead;

	private GameObject[] layers;


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

				layers = GameObject.FindGameObjectsWithTag("Background");
				for(int i = 0; i<layers.Length; i++){
					layers[i].GetComponent<BackgroundScroller>().enabled = false;
				}
			}
		}else if(other.tag != "destructor"){
			Destroy(other.gameObject);
		}    
	}
}