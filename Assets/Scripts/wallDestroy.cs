using UnityEngine;
using System.Collections;

public class wallDestroy : MonoBehaviour {



	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			Destroy(gameObject, 2.0f);
		}    
	}
}
