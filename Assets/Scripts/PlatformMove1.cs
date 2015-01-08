using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour {

	private float Xpos;
	private float Ypos;
	private bool max;

	public bool Vert;
	public int maxAmount;
	public float step;

	// Use this for initialization
	void Start () {
		Xpos = transform.position.x;
		Ypos = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Set Max
		if(Vert) { //Vertical
			if(transform.position.y >= Ypos+maxAmount)
				max = true;
			else if(transform.position.y <= Ypos)
				max = false;
		} else { //Horizontal
			if(transform.position.x >= Xpos+maxAmount)
				max = true;
			else if(transform.position.x <= Xpos)
				max = false;
		}

		//Moving the Platform
		if(Vert) {//Vertical movement
			if(!max) 
				transform.position.y += step;
			else 
				transform.position.y -= step;
		} else {
			if(!max)
				transform.position.x += step;
			else
				transform.position.x -= step;
		}
	}
}
