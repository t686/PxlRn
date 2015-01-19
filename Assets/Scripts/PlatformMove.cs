using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour {

	public bool goVert = false;
	public int maxAmount = 4;
	public float step = 0.05f;

	private float xPos = 0;
	private float currentX = 0;
	private float yPos = 0;
	private float currentY = 0;
	private bool atMaxPosition = false;

	void Start () {
		xPos = transform.position.x;
		yPos = transform.position.y;
		currentX = transform.position.x;
		currentY = transform.position.y;
	}
	
	void FixedUpdate () {
		//SET THE MAX
		if(goVert){ //Vertical
			if(currentY >= yPos + maxAmount){
				atMaxPosition = true;
			} else if (currentY <= yPos){
				atMaxPosition = false;
			}
		} else { //Horizontal
			if(currentX >= xPos + maxAmount){

				atMaxPosition = true;
			} else if (currentX <= xPos){
				atMaxPosition = false;
			}
		}
		
		//MOVING THE PLATFORM
		if(goVert){//Vertical movement
			if(!atMaxPosition){
				currentY += step;
				transform.position = new Vector3(currentX, currentY); 
			}else {
				currentY -= step;
				transform.position = new Vector3(currentX, currentY); 
			}
		} else {

			if(!atMaxPosition){
				currentX += step;
				transform.position = new Vector3(currentX, currentY); 
			}else {
				currentX -= step;
				transform.position = new Vector3(currentX, currentY); 
			}
		}
		
	}
}