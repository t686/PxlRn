#pragma strict

//var Player : GameObject;
//var spawnPoint : Transform;

function OnTriggerEnter2D(other : Collider2D){
	if(other.tag == "Player"){ // restart level if hero indicated
		ReloadGame();
	}else if(other.tag != "destructor"){ // do not delete the Pit object
		Destroy(other.gameObject);
	}
}

function ReloadGame(){			
	// ... pause briefly	
	//yield WaitForSeconds(0.5);
	// ... and then reload the level.
	Application.LoadLevel(Application.loadedLevel);
}
