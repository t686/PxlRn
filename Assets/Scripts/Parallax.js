#pragma strict

private var X : float;
public var offset : int;
public var FollowCamera : boolean;
private var Player : GameObject;
private var fo : GameObject;

function Start () {
	fo = GameObject.FindWithTag("Player");
	X = fo.transform.position.x; //camera starting position
}

function Update () {
	Player = GameObject.FindWithTag("Player");
	if(FollowCamera){
		transform.position.x = (Player.transform.position.x - X)/offset;
	} else {
		transform.position.x = (X - Player.transform.position.x)/offset;
	}
}