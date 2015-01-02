using UnityEngine;
using System.Collections;

public class CollectLoot : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			PlayerControl.currScore += 1;
			audio.enabled = true;
			audio.Play();
			Destroy(this.gameObject);
		}
	}
}
