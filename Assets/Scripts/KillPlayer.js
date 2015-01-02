#pragma strict

//var Player : GameObject;
//var spawnPoint : Transform;

function OnTriggerEnter2D(other : Collider2D){
	if(other.tag == "Player"){ // restart level if hero indicated
		ReloadGame();
	}else if(other.tag != "destructor"){ // do not delete the Pit object
		Destroy(other.gameObject);
	}
	
	//RespawnPlayer();
}

/*function RespawnPlayer(){
	yield WaitForSeconds (0.5);
	var newPlayer : GameObject = Instantiate(Player, spawnPoint.position, Quaternion.identity);
	var sf = Camera.main.GetComponent(CameraFollow);
	sf.target = newPlayer.transform;
}*/

function ReloadGame()
	{			
		// ... pause briefly
		//yield WaitForSeconds(0.5);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
